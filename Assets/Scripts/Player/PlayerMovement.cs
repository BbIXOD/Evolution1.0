using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IControllable
{
    private Rigidbody _rigidbody;
    private PlayerMovementData _playerMovementData;

    [NonSerialized]public AttackManager attackManager;

    [NonSerialized]public float speed;
    private Vector3 _targetRotation;

    private const float MinRotY = 90;
    private const float MaxRotY = 270;
    
    private void Awake()
    {
        var trans = transform;
        
        _rigidbody = GetComponent<Rigidbody>();
        _playerMovementData = new PlayerMovementData();
        attackManager = new AttackManager();

        _rigidbody.angularDrag = Mathf.Infinity;

        _targetRotation = trans.eulerAngles;
    }

    public void Look(Vector2 mPos)
    {
        _targetRotation.x = CheckSector(_targetRotation.x - mPos.y, MinRotY, MaxRotY);
        _targetRotation.y += mPos.x;

        var t = transform;
        var angles = t.eulerAngles;
        t.eulerAngles = Vector3.Lerp(angles, angles + _targetRotation, PlayerMovementData.RotationTime * Time.fixedDeltaTime);
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, PlayerMovementData.RotationTime * Time.fixedDeltaTime);

    }

    public void Forward(float moving)
    {
        speed = Mathf.Lerp(speed, moving * _playerMovementData.speed, _playerMovementData.acceleration);
        _rigidbody.velocity = (speed * Time.fixedDeltaTime * transform.TransformDirection(Vector3.forward));
    }

    public void Attack()
    {
        attackManager.Attack();
    }

    private float CheckSector( float value, float fromZero, float fromFull)
    {
        if (value < fromZero || value > fromFull)
        {
            return value;
        }
        if (Mathf.Abs(value - fromFull) < 1)
        {
            return fromFull - Mathf.Epsilon;
        }
        return fromZero - Mathf.Epsilon;
    }
}
