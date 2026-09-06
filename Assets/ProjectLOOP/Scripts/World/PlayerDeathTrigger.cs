using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectLOOP
{
    [RequireComponent(typeof(Collider))]
    public sealed class PlayerDeathTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (GameSession.Instance != null)
            {
                GameSession.Instance.HandlePlayerDeath();
            }

            SceneManager.LoadScene(GameSession.TownSceneName);
        }
    }
}
