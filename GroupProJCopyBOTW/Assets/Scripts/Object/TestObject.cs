using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour, IMagnetic, IDestructible
{
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    public void Destory()
    {
    }


    public void Movement()
    {
    }



    private void OnCollisionExit(Collision collision)
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    public void Attach()
    {
        rigid.useGravity = false;

    }

    public void Detach()
    {
        rigid.useGravity = true;
    }
}
