using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    Transform skillRoot;
    public Transform SkillRoot => skillRoot;
    Player player;

    public Action usingSkill;
    public Action cancelSkill;

    SkillName currentSkill = SkillName.RemoteBomb;

    private void Awake()
    {
        skillRoot = GetComponentInChildren<HandRoot>().transform;
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
            HandRootTracker handRootTracker = GetComponentInChildren<HandRootTracker>();

            player.onSkillSelect += (skillName) => currentSkill = skillName;
            player.onSkill += OnSkill;
            player.onSkill += () => handRootTracker.OnTracking(skillRoot);
            cancelSkill += handRootTracker.OffTracking;
        }
    }

    void OnSkill()
    {
        switch(currentSkill)
        {
            case SkillName.RemoteBomb:
                SkillFactory.Instance.GetRemoteBomb();
                break;
            case SkillName.MagnetCatch:
                SkillFactory.Instance.GetMagnetCatch();
                break;
            case SkillName.IceMaker:
                break;
            case SkillName.TimeLock: 
                break;
        }
        //startSkill?.Invoke();
    }

#if UNITY_EDITOR

    void TestSkillSelect(SkillName skillName)
    {
        Debug.Log(skillName);
    }

#endif
}
