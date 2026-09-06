using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Builds a playable town stub: floor, player, dungeon portal, HUD.
    /// </summary>
    public sealed class TownStubBootstrap : MonoBehaviour
    {
        void Start()
        {
            PlaceholderFactory.CreateGround(
                "TownFloor",
                Vector3.zero,
                new Vector3(24f, 0.2f, 24f),
                new Color(0.35f, 0.55f, 0.35f));

            var player = PlaceholderFactory.CreatePlayer(new Vector3(0f, 1f, -4f));
            PlaceholderFactory.SetupTopDownCamera(player.transform);

            PlaceholderFactory.CreatePortal(
                "DungeonEntrance",
                new Vector3(0f, 0.2f, 8f),
                GameSession.DungeonSceneName,
                depositLoot: false,
                new Color(0.4f, 0.35f, 0.85f));

            var hud = gameObject.AddComponent<StubHud>();
            hud.SetLocationLabel("Town");
        }
    }
}
