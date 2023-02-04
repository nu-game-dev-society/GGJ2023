using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeedWacker : Weapon
{
    private InputAction FireAction;
    private void Start()
    {
        GameManager.Instance.Controls.controls.Gameplay.Fire.performed += Fire;
    }


    public void Fire(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Performed)
        {
            Fire();
        }
    }
    public override void Fire()
    {
        Debug.Log("Fire");
    }
    public override void Reload()
    {
    }

}
