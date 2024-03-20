using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillName
{
    RemoteBomb = 1,
    RemoteBomb_Cube = 2,
    MagnetCatch = 3,
    TimeLock = 4,
    IceMaker = 5
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
        /*owner.SkillController.startSkill = StartSkillAction;
        owner.SkillController.usingSkill = UseSkillAction;
        owner.SkillController.cancelSkill = EndSkillAction;*/
        originParent = transform.parent;

        transform.parent = skillController.SkillRoot;
        transform.position = skillController.SkillRoot.position;
        transform.forward = owner.transform.forward;

    }

    protected virtual void StartSkillAction()
    {
    }

    protected virtual void UseSkillAction()
    {

    }

    protected virtual void EndSkillAction()
    {

    }
}
