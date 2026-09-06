using UnityEngine;

namespace ProjectLOOP
{
    public sealed class StubHud : MonoBehaviour
    {
        [SerializeField] string locationLabel = "Location";
        Health _playerHealth;

        public void SetLocationLabel(string label)
        {
            locationLabel = label;
        }

        void OnGUI()
        {
            if (_playerHealth == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    _playerHealth = player.GetComponent<Health>();
                }
            }

            var session = GameSession.Instance;
            var townGold = session != null ? session.TownWallet.Gold : 0;
            var runLoot = session != null ? session.RunInventory.Loot : 0;
            var hp = _playerHealth != null
                ? $"{_playerHealth.CurrentHealth}/{_playerHealth.MaxHealth}"
                : "-/-";

            const int pad = 12;
            GUI.Box(new Rect(pad, pad, 400, 140), "Project LOOP");
            GUI.Label(new Rect(pad + 12, pad + 28, 370, 20), $"Location: {locationLabel}");
            GUI.Label(new Rect(pad + 12, pad + 48, 370, 20), $"HP: {hp}");
            GUI.Label(new Rect(pad + 12, pad + 68, 370, 20), $"Town Gold (persistent): {townGold}");
            GUI.Label(new Rect(pad + 12, pad + 88, 370, 20), $"Run Loot (lost on death): {runLoot}");
            GUI.Label(new Rect(pad + 12, pad + 108, 370, 20), "Move: WASD | Attack: LMB / Space / J");
        }
    }
}
