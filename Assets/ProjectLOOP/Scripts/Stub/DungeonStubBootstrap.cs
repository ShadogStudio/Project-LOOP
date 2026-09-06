using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Builds a playable dungeon stub: loot, return portal (deposits), death zone (clears run loot).
    /// </summary>
    public sealed class DungeonStubBootstrap : MonoBehaviour
    {
        void Start()
        {
            PlaceholderFactory.CreateGround(
                "DungeonFloor",
                Vector3.zero,
                new Vector3(24f, 0.2f, 24f),
                new Color(0.28f, 0.28f, 0.34f));

            var player = PlaceholderFactory.CreatePlayer(new Vector3(0f, 1f, -6f));
            PlaceholderFactory.SetupTopDownCamera(player.transform);

            PlaceholderFactory.CreateLoot(new Vector3(-4f, 0.5f, 0f), 10);
            PlaceholderFactory.CreateLoot(new Vector3(4f, 0.5f, 2f), 15);
            PlaceholderFactory.CreateLoot(new Vector3(0f, 0.5f, 4f), 20);

            PlaceholderFactory.CreatePortal(
                "TownReturn",
                new Vector3(0f, 0.2f, -9f),
                GameSession.TownSceneName,
                depositLoot: true,
                new Color(0.3f, 0.75f, 0.45f));

            PlaceholderFactory.CreateDeathZone(new Vector3(8f, 0.2f, 8f));

            var hud = gameObject.AddComponent<StubHud>();
            hud.SetLocationLabel("Dungeon");
        }
    }
}
