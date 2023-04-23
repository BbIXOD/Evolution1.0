using Photon.Pun;
using UnityEngine;

public class Chunk
{
    private int _playersCount;

    private readonly GameObject _field;
    private GameObject _foodController;
    private readonly Terrain _terrain;

    private readonly TerrainData _terrainData;
    private (int, int) _key;

    public static void MakeChunk(int x, int z, GameObject foodController)
    {
        var key = (x, z);
        var chunk = new Chunk(x * ChunkManager.Size.x, z * ChunkManager.Size.z);
        
        ChunkManager.Loaded.Add(key, chunk);
        chunk._key = key;

        chunk._foodController = PhotonNetwork.Instantiate(foodController.name,
            chunk._field.transform.position, Quaternion.Euler(0,0,0));
    }

    private Chunk(float x, float z)
    {
        _playersCount = 1;
        _field = Generate(x, z);
        _terrain = _field.GetComponent<Terrain>();
        _terrainData = _terrain.terrainData;

        var pos = _field.transform.position;
        
        SetChunkHeights.MakeChunk(_terrainData, pos);
    }

    public void AddPlayer()
    {
        _playersCount++;
    }

    public void SubtractPlayer()
    {
        _playersCount--;

        if (_playersCount != 0)
        {
            return;
        }
        
        Object.Destroy(_field);
        Object.Destroy(_foodController);
        ChunkManager.Loaded.Remove(_key);
    }

    public float GetChunkHeight(Vector3 pos)
    {
        return _terrain.SampleHeight(pos);
    }

    private GameObject Generate(float x, float z)
    {
        var terData = new TerrainData
        {
            size = ChunkManager.Size,
            heightmapResolution = (int)ChunkManager.Size.x,
        };

        var pos = new Vector3(x, 0, z);


        var terrain = Terrain.CreateTerrainGameObject(terData);
        
        terrain.transform.position = pos;

        return terrain;
    }
}
