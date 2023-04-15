using UnityEngine;

public class PlayerData
{
    public float rotationTime = 6f;
    public float speed = 100f;
    public float acceleration = 1;
    private static PlayerData _self;

    public static PlayerData Get()
    {
        _self ??= new PlayerData();
        return _self;
    }
    
}
