using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectLOOP
{
    [RequireComponent(typeof(Health))]
    public sealed class PlayerDeathHandler : MonoBehaviour
    {
        Health _health;

        void Awake()
        {
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

        void OnDied(Health _)
        {
            GameSession.Instance?.HandlePlayerDeath();
            SceneManager.LoadScene(SceneNames.Town);
        }
    }
}
