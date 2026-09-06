using System.Collections.Generic;
using UnityEngine;

namespace ProjectLOOP
{
    public enum DungeonMarkerKind
    {
        Entrance,
        Exit,
        EnemySpawn,
        LootSpawn
    }

    public readonly struct DungeonRoomSpec
    {
        public readonly Vector2Int Grid;
        public readonly Vector3 WorldCenter;
        public readonly Vector2 Size;

        public DungeonRoomSpec(Vector2Int grid, Vector3 worldCenter, Vector2 size)
        {
            Grid = grid;
            WorldCenter = worldCenter;
            Size = size;
        }
    }

    public readonly struct DungeonCorridorSpec
    {
        public readonly Vector2Int From;
        public readonly Vector2Int To;
        public readonly Vector3 WorldCenter;
        public readonly Vector3 Size;

        public DungeonCorridorSpec(Vector2Int from, Vector2Int to, Vector3 worldCenter, Vector3 size)
        {
            From = from;
            To = to;
            WorldCenter = worldCenter;
            Size = size;
        }
    }

    public readonly struct DungeonMarker
    {
        public readonly DungeonMarkerKind Kind;
        public readonly Vector3 Position;

        public DungeonMarker(DungeonMarkerKind kind, Vector3 position)
        {
            Kind = kind;
            Position = position;
        }
    }

    /// <summary>
    /// Generator output consumed by gameplay. Provider-agnostic.
    /// </summary>
    public sealed class DungeonLayout
    {
        public readonly IReadOnlyList<DungeonRoomSpec> Rooms;
        public readonly IReadOnlyList<DungeonCorridorSpec> Corridors;
        public readonly IReadOnlyList<DungeonMarker> Markers;
        public readonly int Seed;

        public DungeonLayout(
            int seed,
            List<DungeonRoomSpec> rooms,
            List<DungeonCorridorSpec> corridors,
            List<DungeonMarker> markers)
        {
            Seed = seed;
            Rooms = rooms;
            Corridors = corridors;
            Markers = markers;
        }
    }
}
