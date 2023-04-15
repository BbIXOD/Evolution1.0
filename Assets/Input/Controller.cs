using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private Controls _controls;
    private IControllable _object;

    private void Awake()
    {
        _controls = new Controls();
        
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Movement.Forward.performed += Forward;
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Forward(InputAction.CallbackContext callbackContext)
    {
        _object.Forward();
    }

    private void Look(InputAction.CallbackContext callbackContext)
    {
        var mPos = _controls.Movement.Look.ReadValue<Vector2>();
        _object.Look(mPos);
    }
}
