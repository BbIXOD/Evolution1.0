using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private Controls _controls;
    private IControllable _object;
    private PhotonView _view;
    private MenuController _menuController;

    private void Awake()
    {
        _controls = new Controls();
        _menuController = new MenuController();
        _object = GetComponent<IControllable>();
        _view  = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        _controls.System.Menu.performed += ShowMenu;
        _controls.Enable();

        Cursor.visible = false;
    }

    private void OnDisable()
    {
        _controls.System.Menu.performed -= ShowMenu;
        _controls.Disable();

        Cursor.visible = true;
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

    private void ShowMenu(InputAction.CallbackContext context)
    {
        if (!_view.IsMine)
        {
            return;
        }
        _menuController.ToggleMenuVisibility();
    }
}
