using UnityEngine;

public class HealthManager : IHealth
{
    private int _health;
    public int Health { get => _health; set => _health = CheckHealth(value); }
    private readonly int _maxHealth;
    
    private readonly GameObject _me;

    public HealthManager(GameObject me, int mHealth)
    {
        _me = me;
        _maxHealth = mHealth;
        _health = _maxHealth;
    }
    private int CheckHealth(int health)
    {
        if (health > _maxHealth)
        {
            return _maxHealth;
        }
        if (health > 0)
        {
            return health;
        }
        
        Object.Destroy(_me);
        return 0;
    }
}
