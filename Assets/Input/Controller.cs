using Photon.Pun;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Controls _controls;
    private IControllable _object;
    private PhotonView _view;

    private void Awake()
    {
        _controls = new Controls();
        _object = GetComponent<IControllable>();
        _view  = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        _controls.Enable();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        _controls.Disable();
        //Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (!_view.IsMine)
        {
            return;
        }
        Look();
    }

    private void FixedUpdate()
    {
        if (!_view.IsMine)
        {
            return;
        }
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
