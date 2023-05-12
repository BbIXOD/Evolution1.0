using System;
using UnityEngine;

public class MenuController
{
    private readonly GameObject _menu;
    
    public MenuController()
    {
        _menu = GameObject.FindWithTag("Menu");
        try
        {
            _menu.SetActive(false);
        }
        catch (NullReferenceException)
        {
            Debug.Log("Wait");
        }
    }

    public void ToggleMenuVisibility()
    {
        var active = _menu.activeSelf;
        Cursor.visible = !active;
        
        _menu.SetActive(!_menu.activeSelf);
    }
}
