using UnityEngine;

public class HealthManager : IHealth
{
    private int _health;
    public int Health { get => _health; set => _health = CheckHealth(value); }
    public readonly int maxHealth;
    
    private readonly GameObject _me;

    public HealthManager(GameObject me, int mHealth)
    {
        _me = me;
        maxHealth = mHealth;
        _health = maxHealth;
    }
    private int CheckHealth(int health)
    {
        if (health > maxHealth)
        {
            return maxHealth;
        }
        if (health > 0)
        {
            return health;
        }
        
        Object.Destroy(_me);
        return 0;
    }
}
