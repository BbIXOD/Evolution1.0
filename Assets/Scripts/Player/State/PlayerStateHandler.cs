using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{
    [SerializeField] private MutationController controller;
    
    public PlayerEvo playerEvo;
    public HealthManager playerHealth;

    public int Health { get => playerHealth.Health; set => playerHealth.Health = value; }

    private void Awake()
    {
        playerEvo = new PlayerEvo(controller);
        playerHealth = new HealthManager(gameObject, 100);
    }
}
