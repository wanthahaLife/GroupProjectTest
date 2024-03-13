using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test03_RemoteBomb : TestBase
{
    public RemoteBomb bomb;
    protected override void OnTest1(InputAction.CallbackContext context)
    {

        bomb.TestSkill();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        bomb.TestOnSkill();
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        bomb.TestRemoteOn();
    }
}
