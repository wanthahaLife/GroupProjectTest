using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test06_ReactionObject : TestBase
{
    public ExplosiveObject obj;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        obj.HitReaction(10000.0f);
    }
}
