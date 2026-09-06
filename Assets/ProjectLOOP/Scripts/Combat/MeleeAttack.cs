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
        [SerializeField] float swingFlashSeconds = 0.12f;

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
            SpawnSwingFlash(origin);

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

        void SpawnSwingFlash(Vector3 origin)
        {
            var flash = GameObject.CreatePrimitive(PrimitiveType.Cube);
            flash.name = "MeleeSwingFlash";
            flash.transform.position = origin;
            flash.transform.rotation = transform.rotation;
            flash.transform.localScale = new Vector3(1.4f, 0.15f, 0.55f);

            var col = flash.GetComponent<Collider>();
            if (col != null)
            {
                Object.Destroy(col);
            }

            var color = CompareTag("Player")
                ? new Color(0.95f, 0.95f, 1f, 0.85f)
                : new Color(1f, 0.45f, 0.35f, 0.85f);
            PlaceholderVisuals.ApplyColor(flash, color);

            Object.Destroy(flash, Mathf.Max(0.05f, swingFlashSeconds));
        }
    }
}
