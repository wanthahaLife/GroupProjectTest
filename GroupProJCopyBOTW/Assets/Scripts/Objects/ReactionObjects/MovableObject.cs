using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : ReactionObject
{
    

    public void ExplosionReaction(Vector3 power)
    {
        rigid.AddForce(power * reducePower , ForceMode.Impulse);
    }
}
