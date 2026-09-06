using System.Collections.Generic;
using UnityEngine;

namespace ProjectLOOP
{
    /// <summary>
    /// v1 simple procedural placement: 5~8 connected rooms with corridors, 1 entrance, 1 exit, sparse spawns.
    /// </summary>
    public sealed class SimpleProceduralDungeonGenerator : IDungeonGenerator
    {
        readonly int _minRooms;
        readonly int _maxRooms;
        readonly float _roomSpacing;
        readonly Vector2 _roomSize;
        readonly float _corridorWidth;

        public SimpleProceduralDungeonGenerator(
            int minRooms = 5,
            int maxRooms = 8,
            float roomSpacing = 16f,
            float corridorWidth = 3.5f)
        {
            _minRooms = Mathf.Max(2, minRooms);
            _maxRooms = Mathf.Max(_minRooms, maxRooms);
            _roomSpacing = roomSpacing;
            _roomSize = new Vector2(10f, 10f);
            _corridorWidth = corridorWidth;
        }

        public DungeonLayout Generate(int seed)
        {
            var rng = new System.Random(seed);
            var roomCount = rng.Next(_minRooms, _maxRooms + 1);
            var rooms = new List<DungeonRoomSpec>(roomCount);
            var corridors = new List<DungeonCorridorSpec>(roomCount);
            var occupied = new HashSet<Vector2Int>();

            var current = Vector2Int.zero;
            occupied.Add(current);
            rooms.Add(MakeRoom(current));

            var directions = new[]
            {
                Vector2Int.up,
                Vector2Int.down,
                Vector2Int.left,
                Vector2Int.right
            };

            while (rooms.Count < roomCount)
            {
                var from = rooms[rng.Next(rooms.Count)].Grid;
                var dir = directions[rng.Next(directions.Length)];
                var next = from + dir;
                if (!occupied.Add(next))
                {
                    continue;
                }

                rooms.Add(MakeRoom(next));
                corridors.Add(MakeCorridor(from, next));
            }

            var markers = new List<DungeonMarker>
            {
                new DungeonMarker(DungeonMarkerKind.Entrance, rooms[0].WorldCenter + Vector3.up * 0.5f),
                new DungeonMarker(DungeonMarkerKind.Exit, rooms[rooms.Count - 1].WorldCenter + Vector3.up * 0.5f)
            };

            for (var i = 1; i < rooms.Count - 1; i++)
            {
                if (rng.NextDouble() > 0.55)
                {
                    continue;
                }

                var offset = RandomOffset(rng);
                var kind = rng.NextDouble() < 0.55 ? DungeonMarkerKind.LootSpawn : DungeonMarkerKind.EnemySpawn;
                markers.Add(new DungeonMarker(kind, rooms[i].WorldCenter + offset));
            }

            EnsureAtLeastOneEnemy(rng, rooms, markers);

            return new DungeonLayout(seed, rooms, corridors, markers);
        }

        static void EnsureAtLeastOneEnemy(
            System.Random rng,
            List<DungeonRoomSpec> rooms,
            List<DungeonMarker> markers)
        {
            for (var i = 0; i < markers.Count; i++)
            {
                if (markers[i].Kind == DungeonMarkerKind.EnemySpawn)
                {
                    return;
                }
            }

            // Prefer a middle room; fall back to any non-entrance room.
            var roomIndex = rooms.Count > 2
                ? rng.Next(1, rooms.Count - 1)
                : Mathf.Min(1, rooms.Count - 1);
            markers.Add(new DungeonMarker(
                DungeonMarkerKind.EnemySpawn,
                rooms[roomIndex].WorldCenter + RandomOffset(rng)));
        }

        static Vector3 RandomOffset(System.Random rng)
        {
            return new Vector3(
                (float)(rng.NextDouble() * 2.5 - 1.25),
                0.5f,
                (float)(rng.NextDouble() * 2.5 - 1.25));
        }

        DungeonRoomSpec MakeRoom(Vector2Int grid)
        {
            var center = new Vector3(grid.x * _roomSpacing, 0f, grid.y * _roomSpacing);
            return new DungeonRoomSpec(grid, center, _roomSize);
        }

        DungeonCorridorSpec MakeCorridor(Vector2Int from, Vector2Int to)
        {
            var a = new Vector3(from.x * _roomSpacing, 0f, from.y * _roomSpacing);
            var b = new Vector3(to.x * _roomSpacing, 0f, to.y * _roomSpacing);
            var center = (a + b) * 0.5f;

            // Overlap into both rooms so there is no gap to fall through.
            var overlap = 1f;
            var length = _roomSpacing - _roomSize.x + overlap * 2f;
            Vector3 size;
            if (from.x != to.x)
            {
                size = new Vector3(length, 0.2f, _corridorWidth);
            }
            else
            {
                size = new Vector3(_corridorWidth, 0.2f, length);
            }

            return new DungeonCorridorSpec(from, to, center, size);
        }
    }
}
