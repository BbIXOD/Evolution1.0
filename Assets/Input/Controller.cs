using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    private void OnDisable()
    {
        _controls.Disable();
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
        var mDelta = Mouse.current.delta.ReadValue();
        _object.Look(mDelta);
    }
}
