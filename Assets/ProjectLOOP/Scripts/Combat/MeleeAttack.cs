using UnityEngine;

namespace ProjectLOOP
{
    public sealed class MeleeAttack : MonoBehaviour
    {
        [SerializeField] float range = 1.7f;
        [SerializeField] int damage = 12;
        [SerializeField] float cooldown = 0.55f;
        [SerializeField] float hitRadius = 0.85f;
        [SerializeField] string targetTag = "Enemy";

        float _nextAttackTime;

        public float Range => range;

        public void Configure(float attackRange, int attackDamage, float attackCooldown, string tag)
        {
            range = attackRange;
            damage = attackDamage;
            cooldown = attackCooldown;
            targetTag = tag;
        }

        public bool TryAttack()
        {
            if (Time.time < _nextAttackTime)
            {
                return false;
            }

            _nextAttackTime = Time.time + cooldown;
            var origin = transform.position + Vector3.up + transform.forward * (range * 0.55f);
            var hits = Physics.OverlapSphere(origin, hitRadius);
            var landed = false;

            for (var i = 0; i < hits.Length; i++)
            {
                var col = hits[i];
                if (col == null || !col.CompareTag(targetTag))
                {
                    continue;
                }

                var health = col.GetComponentInParent<Health>();
                if (health == null || health.IsDead)
                {
                    continue;
                }

                health.TakeDamage(damage);
                landed = true;
            }

            return landed;
        }
    }
}
