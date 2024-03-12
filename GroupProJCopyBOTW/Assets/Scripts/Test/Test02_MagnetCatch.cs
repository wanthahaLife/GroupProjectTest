using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test02_MagnetCatch : TestBase
{
    public MagnetCatch magnet;
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        GameManager.Instance.Player.onSkill?.Invoke();
    }
}
