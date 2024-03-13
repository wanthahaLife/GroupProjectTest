using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReactionObj_Mov : MonoBehaviour, IMovable
{
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void MoveForce(Vector3 force)
    {
        rigid.AddForce(force, ForceMode.Impulse);
    }
}
