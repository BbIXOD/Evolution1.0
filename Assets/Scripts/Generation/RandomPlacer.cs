using UnityEngine;

public static class RandomPlacer
{
    public static Vector3 GetPlace(Vector3 position, out bool success, float upTo = 0)
    {
        var locX = (int)position.x;
        var locZ = (int)position.z;
        
        var key = (locX / (int)ChunkManager.Size.x, locZ / (int)ChunkManager.Size.z);
        
        if (!ChunkManager.Loaded.ContainsKey(key))
        {
            success = false;
            return Vector3.zero;
        }
        
        var place = Vector3.zero;
        
        place.x = Random.Range(locX, locX + ChunkManager.Size.x);
        place.z = Random.Range(locZ, locZ + ChunkManager.Size.z);
        
        var minY = ChunkManager.Loaded[key].GetChunkHeight(place);
        place.y = Random.Range(minY, ChunkManager.MaxHeight);
        place.y += upTo;

        success = true;
        return place;
    }
    
    public static Vector3 GetPlace(Vector3 position, out bool success, out float minY, float upTo = 0)
    {
        var locX = (int)position.x;
        var locZ = (int)position.z;
        
        var key = (locX / (int)ChunkManager.Size.x, locZ / (int)ChunkManager.Size.z);
        
        if (!ChunkManager.Loaded.ContainsKey(key))
        {
            minY = 0;
            success = false;
            return Vector3.zero;
        }
        
        var place = Vector3.zero;
        
        place.x = Random.Range(locX, locX + ChunkManager.Size.x);
        place.z = Random.Range(locZ, locZ + ChunkManager.Size.z);
        
        minY = ChunkManager.Loaded[key].GetChunkHeight(place);
        place.y = Random.Range(minY, ChunkManager.MaxHeight);
        place.y += upTo;

        success = true;
        return place;
    }
}
