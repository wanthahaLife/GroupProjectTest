using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public enum SkillName
    {
        RemoteBomb,
        RemoteBomb_Cube,
        MagnetCatch,
        TimeLock,
        IceMaker
    }
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
