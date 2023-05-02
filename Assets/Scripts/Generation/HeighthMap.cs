using UnityEngine;
using Random = UnityEngine.Random;

public static class SetChunkHeights
{
    private const int StartDelta = 1000;

    private const float Scale = 0.07f,
        Power = 3;

    private readonly static int
        DeltaX = Random.Range(-StartDelta, StartDelta);

    private readonly static int
        DeltaZ = Random.Range(-StartDelta, StartDelta);

    public static void MakeChunk(TerrainData data, Vector3 pos)
    {
        const int size = ChunkManager.Resolution + 1;
        var heightMap = new float[size, size];

        for (int x = 0; x < heightMap.GetLength(0); x++)
        {
            for (int z = 0; z < heightMap.GetLength(1); z++)
            {
                heightMap[z, x] =
                    Mathf.PerlinNoise((x * ChunkManager.Scale[0] + pos.x) * Scale + DeltaX, (z * ChunkManager.Scale[0] + pos.z) * Scale + DeltaZ);
                heightMap[z, x] = Mathf.Pow(heightMap[z, x], Power);
            }
        }

        data.SetHeights(0, 0, heightMap);
    }
}
