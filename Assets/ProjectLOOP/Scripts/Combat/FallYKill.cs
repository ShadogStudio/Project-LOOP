using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Backup: kills the actor if it falls below a world Y threshold.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public sealed class FallYKill : MonoBehaviour
    {
        [SerializeField] float killBelowY = -8f;

        Health _health;

        void Awake()
        {
            _health = GetComponent<Health>();
        }

        void Update()
        {
            if (_health.IsDead)
            {
                return;
            }

            if (transform.position.y < killBelowY)
            {
                _health.TakeDamage(_health.CurrentHealth);
            }
        }
    }
}
