using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SkillReactionObj : MonoBehaviour, ISkillAffected
{
    [Flags]
    public enum ReactionType
    {
        Move = 1,
        Destroy = 2,
        Magnetic = 4,
    }

    public ReactionType reactionType;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision collision)
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    public void OnSkillAffect(SkillName skillName)
    {
        switch(skillName)
        {
            case SkillName.RemoteBomb:
                if ((reactionType & ReactionType.Destroy) != 0)
                {
                    Destroy(gameObject);
                }
                else if(((reactionType & ReactionType.Move) != 0))
                {

                }
                    break;
            case SkillName.MagnetCatch:
                if ((reactionType & ReactionType.Magnetic) != 0)
                {
                    rigid.useGravity = false;
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
                }
                break;
            case SkillName.IceMaker:
                break;
            case SkillName.TimeLock:
                break;
        }
    }
}
