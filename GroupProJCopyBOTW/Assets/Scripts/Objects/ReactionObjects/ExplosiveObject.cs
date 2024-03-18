using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : DestructibleObject
{
    protected override void ObjectDestroy()
    {
        // boom 구현
        base.ObjectDestroy();
    }
}
