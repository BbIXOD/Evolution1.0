using System.Collections.Generic;
using UnityEngine;

public static class ChunkManager
{
    public static readonly Dictionary<(int, int), Chunk> Loaded = new ();

    public const int Resolution = 32;
    public static readonly int[] Scale = { 1, 10 };
    public static readonly Vector3 Size = new(Resolution * Scale[0], Scale[1], Resolution * Scale[0]);
    public static readonly float MaxHeight = Scale[1] * 1.5f;

    public const int FoodCount = 25;
}
