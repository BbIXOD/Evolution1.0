
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IControllable
{
    private CharacterController _characterController;
    [SerializeField] private PlayerData _playerData;
    
    private float _speed;
    private bool _swimming;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerData = PlayerData.Get();
    }

    private void FixedUpdate()
    {
        Move();
        _swimming = false;
    }

    public void Forward()
    {
        _swimming = true;
    }
    
    public void Look(Vector2 mousePos)
    {
        var lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.LookAt(lookPos);
    }

    private void Move()
    {
        _speed = Mathf.Lerp(_speed, _swimming ? _playerData.speed : 0, _playerData.acceleration);
        _characterController.Move(_speed * Time.fixedDeltaTime * Vector3.forward);
    }
}
