using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionObject : RecycleObject
{
    public float weight = 1.0f;

    protected float reducePower = 1.0f;
    protected Rigidbody rigid;

    private void Awake()
    {
        reducePower /= weight;
        rigid = GetComponent<Rigidbody>();
    }
}
