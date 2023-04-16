
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IControllable
{
    private Rigidbody _rigidbody;
    private PlayerData _playerData;
    [SerializeField] private Camera _camera;
    
    
    private float _speed;
    private Vector3 _rotationDelta;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerData = new PlayerData();
        _camera = Camera.main;
    }

    public void Look(Vector2 mPos)
    {
        Debug.Log(mPos);
        var delta = Vector3.left *mPos.x; 
        delta += Vector3.up * mPos.y;
        delta *= Time.deltaTime * _playerData.rotationSpeed;
        transform.Rotate(delta);

    }

    public void Forward(float moving)
    {
        _speed = Mathf.Lerp(_speed, moving * _playerData.speed, _playerData.acceleration);
        _rigidbody.velocity = (_speed * Time.fixedDeltaTime * transform.TransformDirection(Vector3.forward));
    }
}
