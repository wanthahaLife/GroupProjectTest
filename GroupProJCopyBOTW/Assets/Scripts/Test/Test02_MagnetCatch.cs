using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test02_MagnetCatch : TestBase
{
    public MagnetCatch magnet;
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        magnet.TestStartSkill();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        magnet.TestUseSkill();
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        magnet.TestFinishSkill();
    }
}
