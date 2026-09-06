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

            GameSession.Instance?.HandlePlayerDeath();
            SceneManager.LoadScene(SceneNames.Town);
        }
    }
}
