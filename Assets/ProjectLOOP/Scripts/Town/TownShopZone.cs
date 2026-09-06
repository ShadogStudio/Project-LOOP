using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectLOOP
{
    /// <summary>
    /// Town shop zone. Player standing inside can buy permanent upgrades with town gold.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public sealed class TownShopZone : MonoBehaviour
    {
        public static TownShopZone Active { get; private set; }

        public string LastMessage { get; private set; } = string.Empty;

        void Reset()
        {
            var col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (!IsPlayer(other))
            {
                return;
            }

            Active = this;
            LastMessage = "Shop ready. Press 1 / 2.";
        }

        void OnTriggerExit(Collider other)
        {
            if (IsPlayer(other) && Active == this)
            {
                Active = null;
                LastMessage = string.Empty;
            }
        }

        void Update()
        {
            if (Active != this || GameSession.Instance == null)
            {
                return;
            }

            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            if (keyboard.digit1Key.wasPressedThisFrame || keyboard.numpad1Key.wasPressedThisFrame)
            {
                GameSession.Instance.TryBuyVitality(out var message);
                LastMessage = message;
                RefreshNearbyPlayerStats();
            }

            if (keyboard.digit2Key.wasPressedThisFrame || keyboard.numpad2Key.wasPressedThisFrame)
            {
                GameSession.Instance.TryBuyPower(out var message);
                LastMessage = message;
                RefreshNearbyPlayerStats();
            }
        }

        static bool IsPlayer(Collider other)
        {
            return other.CompareTag("Player") || other.GetComponentInParent<PlayerMeleeInput>() != null;
        }

        static void RefreshNearbyPlayerStats()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var session = GameSession.EnsureExists();
            if (player == null)
            {
                return;
            }

            var health = player.GetComponent<Health>();
            if (health != null && !health.IsDead)
            {
                health.Configure(session.EffectiveMaxHp);
            }

            player.GetComponent<MeleeAttack>()?.Configure(1.7f, session.EffectiveMeleeDamage, 0.5f, "Enemy");
        }
    }
}
