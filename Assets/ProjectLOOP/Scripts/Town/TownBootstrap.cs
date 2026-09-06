using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Town hub: floor, player, dungeon entrance. Placeholder art only.
    /// </summary>
    public sealed class TownBootstrap : MonoBehaviour
    {
        void Start()
        {
            CreateGround("TownFloor", Vector3.zero, new Vector3(28f, 0.2f, 28f), new Color(0.35f, 0.55f, 0.35f));

            var player = PlaceholderVisuals.CreatePlayer(
                new Vector3(0f, PlaceholderVisuals.FloorSurfaceY() + 0.05f, -5f));
            PlaceholderVisuals.SetupTopDownCamera(player.transform);

            CreatePortal(
                "DungeonEntrance",
                new Vector3(0f, 0.25f, 9f),
                SceneNames.Dungeon,
                depositLoot: false,
                new Color(0.45f, 0.35f, 0.9f));

            PlaceholderVisuals.CreateOutOfBoundsKillVolume(
                transform,
                new Vector3(0f, -12f, 0f),
                new Vector3(80f, 4f, 80f));

            var hud = gameObject.AddComponent<StubHud>();
            hud.SetLocationLabel("Town");
        }

        static void CreateGround(string name, Vector3 position, Vector3 scale, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.position = position;
            go.transform.localScale = scale;
            PlaceholderVisuals.ApplyColor(go, color);
        }

        static void CreatePortal(string name, Vector3 position, string scene, bool depositLoot, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.position = position;
            go.transform.localScale = new Vector3(2.4f, 0.35f, 2.4f);
            var col = go.GetComponent<BoxCollider>();
            col.isTrigger = true;
            go.AddComponent<ScenePortal>().Configure(scene, depositLoot);
            PlaceholderVisuals.ApplyColor(go, color);
        }
    }
}
