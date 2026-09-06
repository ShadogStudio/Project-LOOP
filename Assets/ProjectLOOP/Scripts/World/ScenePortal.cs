using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectLOOP
{
    [RequireComponent(typeof(Collider))]
    public sealed class ScenePortal : MonoBehaviour
    {
        [SerializeField] string targetSceneName;
        [SerializeField] bool depositRunLootOnEnter;

        public void Configure(string sceneName, bool depositLoot)
        {
            targetSceneName = sceneName;
            depositRunLootOnEnter = depositLoot;
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (depositRunLootOnEnter && GameSession.Instance != null)
            {
                GameSession.Instance.DepositRunLootToTown();
            }

            SceneManager.LoadScene(targetSceneName);
        }
    }
}
