using UnityEngine;

public class PlayerHealth
{
    private int _health;
    public int Health { get => _health; set => _health = CheckHealth(value); }
    public const int MaxHealth = 100;
    
    private readonly GameObject _player;

    public PlayerHealth(GameObject player)
    {
        _player = player;
    }
    private int CheckHealth(int health)
    {
        switch (health)
        {
            case > MaxHealth:
                return MaxHealth;
            case > 0:
                return health;
            default:
                Object.Destroy(_player);
                return 0;
        }
    }
}
