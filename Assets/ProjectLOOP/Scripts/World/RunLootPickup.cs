using UnityEngine;

namespace ProjectLOOP
{
    [RequireComponent(typeof(Collider))]
    public sealed class RunLootPickup : MonoBehaviour
    {
        [SerializeField] int amount = 10;

        public void Configure(int lootAmount)
        {
            amount = lootAmount;
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            GameSession.Instance?.RunInventory.Add(amount);
            Destroy(gameObject);
        }
    }
}
