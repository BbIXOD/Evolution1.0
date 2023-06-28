using UnityEngine;

public class HealthBeh : MonoBehaviour, IHealth
{
    [SerializeField] private int health = 100;
    public HealthManager manager;
    
    public int Health {get => manager.Health; set => manager.Health = value; }

    private void Awake()
    {
        manager = new HealthManager(gameObject, health);
    }
}

