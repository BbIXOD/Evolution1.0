using UnityEngine;

public class PlayerStateGetter
{

    private readonly PlayerMovement _movement;
    private readonly PlayerStateHandler _handler;

    public PlayerStateGetter(GameObject player)
    {
        _movement = player.GetComponent<PlayerMovement>();
        _handler = player.GetComponent<PlayerStateHandler>();
    }
    
    public float Moving()
    {
        return _movement.speed;
    }

    public float HowHurt()
    {
        return (_handler.playerHealth.maxHealth - _handler.Health) * Time.fixedDeltaTime;
    }
}
