using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test99_Rigidbody : TestBase
{
    public ReactionObject r;
    public Transform target;

    bool isOn = false;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
       isOn = !isOn;
    }

    private void FixedUpdate()
    {
        if (isOn)
            r.AttachMagnetMove(target.position, Vector3.zero, 3.0f);
    }
}
