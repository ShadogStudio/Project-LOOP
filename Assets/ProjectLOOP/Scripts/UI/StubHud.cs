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
            var bonusHp = session != null ? session.BonusMaxHp : 0;
            var bonusDmg = session != null ? session.BonusMeleeDamage : 0;
            var hp = _playerHealth != null
                ? $"{_playerHealth.CurrentHealth}/{_playerHealth.MaxHealth}"
                : "-/-";

            const int pad = 12;
            var height = TownShopZone.Active != null ? 210 : 140;
            GUI.Box(new Rect(pad, pad, 400, height), "Project LOOP");
            GUI.Label(new Rect(pad + 12, pad + 28, 370, 20), $"Location: {locationLabel}");
            GUI.Label(new Rect(pad + 12, pad + 48, 370, 20), $"HP: {hp}  (bonus HP +{bonusHp}, dmg +{bonusDmg})");
            GUI.Label(new Rect(pad + 12, pad + 68, 370, 20), $"Town Gold (persistent): {townGold}");
            GUI.Label(new Rect(pad + 12, pad + 88, 370, 20), $"Run Loot (lost on death): {runLoot}");
            GUI.Label(new Rect(pad + 12, pad + 108, 370, 20), "Move: WASD | Attack: LMB / Space / J");

            if (TownShopZone.Active != null)
            {
                var shop = TownShopZone.Active;
                GUI.Label(new Rect(pad + 12, pad + 132, 370, 20),
                    $"[1] Vitality +{GameSession.VitalityBonus} Max HP ({GameSession.VitalityCost}g)");
                GUI.Label(new Rect(pad + 12, pad + 152, 370, 20),
                    $"[2] Power +{GameSession.PowerBonus} Damage ({GameSession.PowerCost}g)");
                GUI.Label(new Rect(pad + 12, pad + 172, 370, 20), shop.LastMessage);
            }
        }
    }
}
