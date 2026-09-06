using UnityEngine;

namespace ProjectLOOP
{
    public sealed class StubHud : MonoBehaviour
    {
        [SerializeField] string locationLabel = "Location";

        public void SetLocationLabel(string label)
        {
            locationLabel = label;
        }

        void OnGUI()
        {
            var session = GameSession.Instance;
            var townGold = session != null ? session.TownWallet.Gold : 0;
            var runLoot = session != null ? session.RunInventory.Loot : 0;

            const int pad = 12;
            GUI.Box(new Rect(pad, pad, 340, 120), "Project LOOP");
            GUI.Label(new Rect(pad + 12, pad + 28, 310, 20), $"Location: {locationLabel}");
            GUI.Label(new Rect(pad + 12, pad + 48, 310, 20), $"Town Gold (persistent): {townGold}");
            GUI.Label(new Rect(pad + 12, pad + 68, 310, 20), $"Run Loot (lost on death): {runLoot}");
            GUI.Label(new Rect(pad + 12, pad + 88, 310, 20), "Move: WASD / Arrows");
        }
    }
}
