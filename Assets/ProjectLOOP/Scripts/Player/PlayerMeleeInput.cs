using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectLOOP
{
    [RequireComponent(typeof(MeleeAttack))]
    [RequireComponent(typeof(Health))]
    public sealed class PlayerMeleeInput : MonoBehaviour
    {
        MeleeAttack _melee;
        Health _health;

        void Awake()
        {
            _melee = GetComponent<MeleeAttack>();
            _health = GetComponent<Health>();
        }

        void Update()
        {
            if (_health.IsDead)
            {
                return;
            }

            if (!WasAttackPressed())
            {
                return;
            }

            _melee.TryAttack();
        }

        static bool WasAttackPressed()
        {
            var mouse = Mouse.current;
            if (mouse != null && mouse.leftButton.wasPressedThisFrame)
            {
                return true;
            }

            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return false;
            }

            return keyboard.jKey.wasPressedThisFrame || keyboard.spaceKey.wasPressedThisFrame;
        }
    }
}
