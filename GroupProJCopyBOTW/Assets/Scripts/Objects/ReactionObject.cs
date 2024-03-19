using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;


[Flags]
public enum ReactionType
{
    Move = 1,
    Throw = 2,
    Magnetic = 4,
    Destroy = 8,
    Explosion = 16
}

[RequireComponent(typeof(Rigidbody))]
public class ReactionObject : RecycleObject
{
    public ExplosiveObject explosiveInfo;

    public float Weight = 1.0f;
    float reducePower = 1.0f;

    bool isCarry = false;

    public ReactionType reactionType;
    public ReactionType Type => reactionType;

    const float MaxDestroyDamage = 1000.0f;

    Transform originParent;

    Rigidbody rigid;


    bool magnetReaction = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        if(rigid == null)
        {
            rigid = transform.AddComponent<Rigidbody>();
        }
        isCarry = false;
        originParent = transform.parent;
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        if (player != null)
        {
        //    player.onThrow += Throw;
        }
        else
        {
            Debug.LogWarning("플레이어가 없습니다.");
        }
    }



    private void OnCollisionExit(Collision collision)
    {
        if (magnetReaction)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    public void Explosion(Vector3 power)
    {
        if ((reactionType & ReactionType.Move) != 0)
        {
            rigid.AddForce(power * reducePower, ForceMode.Impulse);
        }
    }

    public void Lift(Transform root)
    {
        if ((reactionType & ReactionType.Throw) != 0)
        {
            transform.parent = root;
            transform.position = root.position;
            isCarry = true;
            rigid.isKinematic = true;
        }
    }

    public void Throw(float throwPower, Transform user)
    {
        if ((reactionType & ReactionType.Throw) != 0 && isCarry)
        {
            rigid.AddRelativeForce((user.forward + user.up) * throwPower, ForceMode.Impulse);
            isCarry = false;
            rigid.isKinematic = false;
        }
    }

    public void Drop()
    {
        if ((reactionType & ReactionType.Throw) != 0)
        {
            transform.parent = originParent;
            isCarry = true;
            rigid.isKinematic = false;
        }
    }

}

[Serializable]
public class ExplosiveObject
{
    public float force = 4.0f;
    public float forceY = 5.0f;
    public float boomRange = 3.0f;
    public float damage = 3.0f;
}
