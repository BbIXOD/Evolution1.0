using UnityEngine;

public class PlayerStateGetter
{

    private PlayerMovement _movement;

    public PlayerStateGetter(GameObject player)
    {

        _movement = player.GetComponent<PlayerMovement>();
    }
    
    public float Moving()
    {
        return _movement.speed;
    }
}
