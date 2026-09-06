using System;
using UnityEngine;

namespace ProjectLOOP
{
    public sealed class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 50;

        public int MaxHealth => maxHealth;
        public int CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }

        public event Action<Health> Damaged;
        public event Action<Health> Died;

        void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void Configure(int max)
        {
            maxHealth = Mathf.Max(1, max);
            CurrentHealth = maxHealth;
            IsDead = false;
        }

        public void TakeDamage(int amount)
        {
            if (IsDead || amount <= 0)
            {
                return;
            }

            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            Damaged?.Invoke(this);

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                Died?.Invoke(this);
            }
        }
    }
}
