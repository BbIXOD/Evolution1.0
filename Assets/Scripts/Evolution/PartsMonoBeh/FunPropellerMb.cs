using UnityEngine;

public class FunPropellerMb : MonoBehaviour
{
    [SerializeField] private int speedMultiplier;

    private PlayerMovement _movement;
    
    private void Start()
    {
        _movement = transform.parent.GetComponent<PlayerMovement>();
        _movement.speed *= speedMultiplier;
    }

    private void OnDestroy()
    {
        if (_movement != null)
        {
            _movement.speed /= 2;
        }
    }
}
