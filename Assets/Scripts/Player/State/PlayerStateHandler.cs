using UnityEngine;

public class PlayerStateHandler : MonoBehaviour, IHealth
{
    [SerializeField] private MutationController controller;

    public PlayerEvo playerEvo;
    public HealthManager playerHealth;
    public PlayerMovementData pMData;

    public int Health { get => playerHealth.Health; set => playerHealth.Health = value; }

    private void Awake()
    {
        playerEvo = new PlayerEvo(controller);
        playerHealth = new HealthManager(gameObject, 100);
        pMData = new PlayerMovementData();
    }
}
