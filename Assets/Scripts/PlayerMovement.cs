
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
        var delta = _camera.ScreenToViewportPoint(mPos);
        delta.x -= 0.5f;
        delta.y -= 0.5f;

        var t = transform;
        var angles = t.eulerAngles;
        var rotation = t.rotation;
        angles = new Vector3(angles.x - delta.y, angles.y + delta.x, 0);

        transform.eulerAngles = angles;
    }

    public void Forward(float moving)
    {
        _speed = Mathf.Lerp(_speed, moving * _playerData.speed, _playerData.acceleration);
        _rigidbody.velocity = (_speed * Time.fixedDeltaTime * transform.TransformDirection(Vector3.forward));
    }
}
