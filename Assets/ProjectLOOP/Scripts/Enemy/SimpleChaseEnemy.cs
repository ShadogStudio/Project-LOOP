using UnityEngine;

namespace ProjectLOOP
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(MeleeAttack))]
    [RequireComponent(typeof(Health))]
    public sealed class SimpleChaseEnemy : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 3.4f;
        [SerializeField] float gravity = -20f;
        [SerializeField] float detectRange = 14f;
        [SerializeField] float attackRange = 1.55f;

        CharacterController _controller;
        MeleeAttack _melee;
        Health _health;
        Transform _target;
        float _verticalVelocity;

        void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _melee = GetComponent<MeleeAttack>();
            _health = GetComponent<Health>();
            _health.Died += OnDied;
        }

        void OnDestroy()
        {
            if (_health != null)
            {
                _health.Died -= OnDied;
            }
        }

        void Update()
        {
            if (_health.IsDead)
            {
                return;
            }

            if (_target == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    _target = player.transform;
                }
            }

            if (_target == null)
            {
                ApplyGravityOnly();
                return;
            }

            var toTarget = _target.position - transform.position;
            toTarget.y = 0f;
            var distance = toTarget.magnitude;

            if (distance > detectRange)
            {
                ApplyGravityOnly();
                return;
            }

            if (distance > 0.05f)
            {
                var dir = toTarget / distance;
                transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

                if (distance > attackRange)
                {
                    Move(dir * moveSpeed);
                }
                else
                {
                    ApplyGravityOnly();
                    _melee.TryAttack();
                }
            }
            else
            {
                ApplyGravityOnly();
            }
        }

        void Move(Vector3 planarVelocity)
        {
            if (_controller.isGrounded && _verticalVelocity < 0f)
            {
                _verticalVelocity = -1f;
            }
            else
            {
                _verticalVelocity += gravity * Time.deltaTime;
            }

            planarVelocity.y = _verticalVelocity;
            _controller.Move(planarVelocity * Time.deltaTime);
        }

        void ApplyGravityOnly()
        {
            Move(Vector3.zero);
        }

        void OnDied(Health _)
        {
            Destroy(gameObject);
        }
    }
}
