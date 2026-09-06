using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// Builds a dungeon run from IDungeonGenerator output using placeholders.
    /// Swap generator later (Dungeon Architect) without changing this flow.
    /// </summary>
    public sealed class DungeonRunController : MonoBehaviour
    {
        [SerializeField] int seed;
        [SerializeField] bool randomizeSeed = true;

        IDungeonGenerator _generator;

        void Awake()
        {
            _generator = new SimpleProceduralDungeonGenerator();
        }

        void Start()
        {
            if (randomizeSeed || seed == 0)
            {
                seed = Random.Range(1, int.MaxValue);
            }

            var layout = _generator.Generate(seed);
            BuildLayout(layout);

            var hud = gameObject.AddComponent<StubHud>();
            hud.SetLocationLabel($"Dungeon (seed {seed})");
        }

        void BuildLayout(DungeonLayout layout)
        {
            var root = new GameObject("GeneratedDungeon").transform;

            foreach (var room in layout.Rooms)
            {
                var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
                floor.name = $"Room_{room.Grid.x}_{room.Grid.y}";
                floor.transform.SetParent(root, false);
                floor.transform.position = room.WorldCenter;
                floor.transform.localScale = new Vector3(room.Size.x, 0.2f, room.Size.y);
                PlaceholderVisuals.ApplyColor(floor, new Color(0.28f, 0.28f, 0.34f));
            }

            foreach (var corridor in layout.Corridors)
            {
                var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
                floor.name = $"Corridor_{corridor.From.x}_{corridor.From.y}_to_{corridor.To.x}_{corridor.To.y}";
                floor.transform.SetParent(root, false);
                floor.transform.position = corridor.WorldCenter;
                floor.transform.localScale = corridor.Size;
                PlaceholderVisuals.ApplyColor(floor, new Color(0.22f, 0.22f, 0.28f));
            }

            PlaceholderVisuals.CreateOutOfBoundsKillVolume(
                root,
                new Vector3(0f, -12f, 0f),
                new Vector3(200f, 4f, 200f));

            Vector3 spawn = layout.Rooms[0].WorldCenter + Vector3.up * (PlaceholderVisuals.FloorSurfaceY() + 0.05f);
            foreach (var marker in layout.Markers)
            {
                switch (marker.Kind)
                {
                    case DungeonMarkerKind.Entrance:
                        spawn = new Vector3(
                            marker.Position.x,
                            PlaceholderVisuals.FloorSurfaceY() + 0.05f,
                            marker.Position.z);
                        break;
                    case DungeonMarkerKind.Exit:
                        CreatePortal(root, "TownReturn", marker.Position, SceneNames.Town, depositLoot: true, new Color(0.3f, 0.75f, 0.45f));
                        break;
                    case DungeonMarkerKind.LootSpawn:
                        CreateLoot(root, marker.Position, 10);
                        break;
                    case DungeonMarkerKind.EnemySpawn:
                        CreateEnemy(root, marker.Position);
                        break;
                }
            }

            var player = PlaceholderVisuals.CreatePlayer(spawn);
            PlaceholderVisuals.SetupTopDownCamera(player.transform);
        }

        static void CreatePortal(Transform parent, string name, Vector3 position, string scene, bool depositLoot, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.SetParent(parent, false);
            go.transform.position = position;
            go.transform.localScale = new Vector3(2.4f, 0.35f, 2.4f);
            go.GetComponent<BoxCollider>().isTrigger = true;
            go.AddComponent<ScenePortal>().Configure(scene, depositLoot);
            PlaceholderVisuals.ApplyColor(go, color);
        }

        static void CreateLoot(Transform parent, Vector3 position, int amount)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.name = "RunLoot";
            go.transform.SetParent(parent, false);
            go.transform.position = position;
            go.transform.localScale = Vector3.one * 0.7f;
            go.GetComponent<SphereCollider>().isTrigger = true;
            go.AddComponent<RunLootPickup>().Configure(amount);
            PlaceholderVisuals.ApplyColor(go, new Color(1f, 0.85f, 0.2f));
        }

        static void CreateEnemy(Transform parent, Vector3 markerPosition)
        {
            var feet = new Vector3(
                markerPosition.x,
                PlaceholderVisuals.FloorSurfaceY() + 0.05f,
                markerPosition.z);
            var enemy = PlaceholderVisuals.CreateEnemy(feet);
            enemy.transform.SetParent(parent, true);
        }
    }
}
