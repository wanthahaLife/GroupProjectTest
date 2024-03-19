using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class ObjectEditor : MonoBehaviour
{
    public float Weight = 1.0f;
    bool carry = false;

#if UNITY_EDITOR
    [Flags]
    public enum ReactionType
    {
        Move = 1,
        Destroy = 2,
        Magnetic = 4,
        Throw = 8,
        Explosion = 16
    }

    public ReactionType reactionType;
    public ReactionType Type => reactionType;

    MovableObject movableObject;
    DestructibleObject destructibleObject;
    MagneticObject magneticObject;
    ThrowableObject throwableObject;
    ExplosiveObject explosiveObject;


    private void OnValidate() => EditorApplication.delayCall += SafeOnValidate;

    private void SafeOnValidate()
    {
        AddObjectComponent();
        RemoveObjectComponent();
    }

    bool magnetReaction = false;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        carry = false;
        AddObjectComponent();
        RemoveObjectComponent();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        if(player != null)
        {
            player.onThrow += Throw;
        }
        else
        {
            Debug.LogWarning("플레이어가 없습니다.");
        }
    }

    void AddObjectComponent()
    {

        movableObject = transform.GetComponent<MovableObject>();
        if (movableObject == null)
        {
            movableObject = transform.AddComponent<MovableObject>();
        }

        destructibleObject = transform.GetComponent<DestructibleObject>();
        if (destructibleObject == null)
        {
            destructibleObject = transform.AddComponent<DestructibleObject>();
        }

        magneticObject = transform.GetComponent<MagneticObject>();
        if (magneticObject == null)
        {
            magneticObject = transform.AddComponent<MagneticObject>();
        }

        throwableObject = transform.GetComponent<ThrowableObject>();
        if (throwableObject == null)
        {
            throwableObject = transform.AddComponent<ThrowableObject>();
        }

        explosiveObject = transform.GetComponent<ExplosiveObject>();
        if( explosiveObject == null)
        {
            explosiveObject = transform.AddComponent<ExplosiveObject>();
        }
    }

    void RemoveObjectComponent()
    {

        if (movableObject != null && (reactionType & ReactionType.Move) == 0)
            DestroyImmediate(movableObject);

        if (destructibleObject != null && (reactionType & ReactionType.Destroy) == 0)
            DestroyImmediate(destructibleObject);
        
        if (magneticObject != null && (reactionType & ReactionType.Magnetic) == 0)
            DestroyImmediate(magneticObject);

        if (throwableObject != null && (reactionType & ReactionType.Throw) == 0)
            DestroyImmediate(throwableObject);

        if (explosiveObject != null && (reactionType & ReactionType.Explosion) == 0)
            DestroyImmediate(explosiveObject);
    }

#endif
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (magnetReaction)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    public void OnSkillAffect(SkillName skillName)
    {
        OnSkillAffect(skillName, Vector3.zero);
    }

    public void OnSkillAffect(SkillName skillName, Vector3 power)
    {
        switch (skillName)
        {
            case SkillName.RemoteBomb:
                if ((reactionType & ReactionType.Destroy) != 0)
                {
                    Destroy(gameObject);
                }
                else if (((reactionType & ReactionType.Move) != 0))
                {
                    rigid.AddForce(power, ForceMode.Impulse);
                }
                break;
            case SkillName.MagnetCatch:
                if ((reactionType & ReactionType.Magnetic) != 0)
                {
                    rigid.useGravity = false;
                    magnetReaction = true;
                }
                break;
            case SkillName.IceMaker:
                break;
            case SkillName.TimeLock:
                break;
        }
    }

    public void FinishSkillAffect(SkillName skillName)
    {
        switch (skillName)
        {
            case SkillName.RemoteBomb:
                break;
            case SkillName.MagnetCatch:
                if ((reactionType & ReactionType.Magnetic) != 0)
                {
                    rigid.useGravity = true;
                    magnetReaction = false;
                }
                break;
            case SkillName.IceMaker:
                break;
            case SkillName.TimeLock:
                break;
        }
    }

    public void Throw(float throwPower)
    {
        if(reactionType == ReactionType.Throw)
        {
            if (carry)
            {
                rigid.isKinematic = false;
                rigid.AddRelativeForce((transform.forward + transform.up) * throwPower, ForceMode.Impulse);
                carry = false;
            }
            if (!carry)
            {
                carry = true;
                rigid.isKinematic = true;
            }
        }
    }
}
