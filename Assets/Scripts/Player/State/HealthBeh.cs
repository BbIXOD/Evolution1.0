using UnityEngine;

public class HealthBeh : MonoBehaviour, IHealth
{
    [SerializeField] private int health = 100;
    private HealthManager _manager;
    
    public int Health {get => _manager.Health; set => _manager.Health = value; }

    private void Awake()
    {
        _manager = new HealthManager(gameObject, health);
    }
}

