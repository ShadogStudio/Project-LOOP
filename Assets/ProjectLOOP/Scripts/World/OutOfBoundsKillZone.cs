using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Instantly kills any Health actor (player or enemy) that enters the volume.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public sealed class OutOfBoundsKillZone : MonoBehaviour
    {
        void Reset()
        {
            var col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        void OnTriggerEnter(Collider other)
        {
            Kill(other);
        }

        void OnTriggerStay(Collider other)
        {
            // CharacterController can miss Enter in some edge cases while falling fast.
            Kill(other);
        }

        static void Kill(Collider other)
        {
            var health = other.GetComponentInParent<Health>();
            if (health == null || health.IsDead)
            {
                return;
            }

            health.TakeDamage(health.CurrentHealth);
        }
    }
}
