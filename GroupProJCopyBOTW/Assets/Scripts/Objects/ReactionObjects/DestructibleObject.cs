using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : ReactionObject
{
    public float objectMaxHp = 1.0f;

    float objectHp;
    protected float OjbectHp
    {
        get => objectHp;
        set
        {
            objectHp = value;
            if(objectHp < 0.0f)
            {
                ObjectDestroy();
            }
        }
    }


    public void HitReaction(float power)
    {
        objectHp -= power;
    }

    public void HitReaction()
    {
        HitReaction(1.0f);
    }

    protected virtual void ObjectDestroy()
    {

    }
}
