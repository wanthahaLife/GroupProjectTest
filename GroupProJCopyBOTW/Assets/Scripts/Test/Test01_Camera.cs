using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test01_Camera : TestBase
{
    public SkillVCam vCam;
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        vCam.TestSkillCamera();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        vCam.TestOriginCamera();
    }
}
