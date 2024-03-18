using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : ReactionObject
{
    public void ThrowReaction(Vector3 power)
    {
        rigid.AddForce(power * reducePower, ForceMode.Impulse);
    }
}
