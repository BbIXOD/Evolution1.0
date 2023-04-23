using System.Collections.Generic;
using UnityEngine;

public static class ChunkManager
{
    public readonly static Dictionary<(int, int), Chunk> Loaded = new Dictionary<(int, int), Chunk>();

    private readonly static int[] SizeBase = {  32, 10 };
    public readonly static Vector3 Size = new Vector3(SizeBase[0], SizeBase[1], SizeBase[0]);
    
    public const int FoodCount = 50;

}
