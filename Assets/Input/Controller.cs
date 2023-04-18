using UnityEngine;

public class Controller : MonoBehaviour
{
    private Controls _controls;
    private IControllable _object;

    private void Awake()
    {
        _controls = new Controls();
        _object = GetComponent<IControllable>();
    }

    private void OnEnable()
    {
        _controls.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        _controls.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        Look();
    }

    private void FixedUpdate()
    {
        Forward();
    }

    private void Forward()
    {
        var moving = _controls.Movement.Forward.ReadValue<float>();
        _object.Forward(moving);
    }

    private void Look()
    {
        var mPos = _controls.Movement.Look.ReadValue<Vector2>();
        _object.Look(mPos);
    }
}
