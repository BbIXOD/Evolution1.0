using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{
    [SerializeField] private MutationController _controller;
    
    public PlayerEvo playerEvo;
    public PlayerHealth playerHealth;

    private void Awake()
    {
        playerEvo = new PlayerEvo(_controller);
        playerHealth = new PlayerHealth(gameObject);
    }
}
