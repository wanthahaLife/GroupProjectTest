using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test07_IceMaker : Skill_TestBase
{
    public IceMaker icemaker;
    IceMaker_Ice iceMaker_Ice;


    private void Start()
    {
        iceMaker_Ice = icemaker.transform.GetChild(1).GetComponentInChildren<IceMaker_Ice>();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
       iceMaker_Ice.SetGenerate();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
       iceMaker_Ice.SetDestroy();
    }
}
