using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    Transform skillRoot;
    public Transform SkillRoot => skillRoot;
    Player player;

    public Action useSkill;
    public Action startSkill;
    public Action endSkill;

    private void Awake()
    {
        skillRoot = GetComponentInChildren<SkillRoot>().transform;
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
        if(player == null)
        {
            Debug.LogError("플레이어가 없습니다.");
        }
        else
        {
            /*player.leftClick += StartSkill;
            player.rightClick += endSkill;
            player.skillSelect += (skillName) => {
                SkillSelect((SkillName)skillName);
                useSkill?.Invoke();
            };*/
            
        }
    }

    void SkillSelect(SkillName skillName)
    {
        switch(skillName)
        {
            case SkillName.RemoteBomb:
                SkillFactory.Instance.GetRemoteBomb();
                break;
            case SkillName.MagnetCatch:
                break;
            case SkillName.IceMaker:
                break;
            case SkillName.TimeLock: 
                break;
        }
    }

#if UNITY_EDITOR
    public void TestSkillSelect()
    {

    }

#endif
}
