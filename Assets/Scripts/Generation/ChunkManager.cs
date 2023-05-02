using System.Collections.Generic;
using UnityEngine;

public static class ChunkManager
{
    public readonly static Dictionary<(int, int), Chunk> Loaded = new Dictionary<(int, int), Chunk>();

    public const int Resolution = 32;
    public readonly static int[] Scale = { 1, 10 };
    public readonly static Vector3 Size = new Vector3(Resolution * Scale[0], Scale[1], Resolution * Scale[0]);
    public readonly static float MaxHeight = Scale[1] * 1.5f;

    public const int FoodCount = 50;
}
