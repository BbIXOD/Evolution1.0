
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IControllable
{
    private Rigidbody _rigidbody;
    private PlayerMovementData _playerMovementData;
    [SerializeField] private Camera _camera;


    private float _speed;
    private Vector3 _rotationDelta;

    private const float minRotY = 90;
    private const float maxRotY = 270;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerMovementData = new PlayerMovementData();
        _camera = Camera.main;
    }

    public void Look(Vector2 mPos)
    {
        var delta = _camera.ScreenToViewportPoint(mPos);
        delta.x -= 0.5f;
        delta.y -= 0.5f;
        
        CheckMin(ref delta.x);
        CheckMin(ref delta.y);
        

        var t = transform;
        var angles = t.eulerAngles;
        angles = new Vector3(CheckSector((angles.x - delta.y), minRotY, maxRotY), angles.y + delta.x, 0);

        t.eulerAngles = angles;
    }

    public void Forward(float moving)
    {
        _speed = Mathf.Lerp(_speed, moving * _playerMovementData.speed, _playerMovementData.acceleration);
        _rigidbody.velocity = (_speed * Time.fixedDeltaTime * transform.TransformDirection(Vector3.forward));
    }

    private void CheckMin(ref float value)
    {
        if (Mathf.Abs(value) < _playerMovementData.minRot)
        {
            value = 0;
        }
    }

    private float CheckSector( float value, float fromZero, float fromFull)
    {
        if (value < fromZero || value > fromFull)
        {
            return value;
        }
        if (Mathf.Abs(value - fromFull) < 1)
        {
            return fromFull - 0.1f;
        }
        return fromZero - 0.1f;
    }
}
