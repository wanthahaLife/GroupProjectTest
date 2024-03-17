using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillName
{
    RemoteBomb = 1,
    RemoteBomb_Cube = 1,
    MagnetCatch = 2,
    TimeLock = 3,
    IceMaker = 4
}

public class Skill : RecycleObject
{
    public SkillName skillName = SkillName.RemoteBomb;
    public float coolTime = 1.0f;
    protected float currCoolTime = 0.0f;
    public bool canUse = false;

    protected Player owner;
    protected Transform originParent = null;
    protected PlayerSkillController skillController;


    protected virtual void Start()
    {
        if (skillController != null)
        {
            owner.SkillController.startSkill += StartSkill;
            owner.SkillController.useSkill += UseSkill;
            owner.SkillController.endSkill += EndSkill;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if(owner == null)
        {
            owner = GameManager.Instance.Player;
        }
        if(skillController == null)
        {
            skillController = owner.SkillController;
        }
        originParent = transform.parent;

        transform.parent = skillController.SkillRoot;
        transform.position = skillController.SkillRoot.position;
        transform.forward = owner.transform.forward;
    }

    protected virtual void StartSkill()
    {
    }

    protected virtual void UseSkill()
    {

    }

    protected virtual void EndSkill()
    {

    }
}
