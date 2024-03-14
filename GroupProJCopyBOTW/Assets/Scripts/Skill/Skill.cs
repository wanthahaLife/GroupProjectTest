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

    protected Player player;

    private void Start()
    {
        player = GameManager.Instance.Player;
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
