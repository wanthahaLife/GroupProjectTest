using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    Transform handRoot;
    public Transform HandRoot => handRoot;
    Player player;

    public Action onSKillAction;
    public Action useSkillAction;
    public Action offSkillAction;

    SkillName currentSkill = SkillName.RemoteBomb;

    private void Awake()
    {
        handRoot = GetComponentInChildren<HandRoot>().transform;
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

            player.onSkillSelect += (skillName) => currentSkill = skillName;

            player.onSkill += () => onSKillAction?.Invoke();
            player.rightClick += () => useSkillAction?.Invoke();
            player.onCancel += () => offSkillAction?.Invoke();

            onSKillAction += OnSkill;

        }
    }

    void OnSkill()
    {
        switch(currentSkill)
        {
            case SkillName.RemoteBomb:
                Debug.Log("리모컨 폭탄 실행");
                SkillFactory.Instance.GetRemoteBomb();
                break;
            case SkillName.RemoteBomb_Cube:
                Debug.Log("리모컨 폭탄 큐브 실행");
                SkillFactory.Instance.GetRemoteBomb();
                break;
            case SkillName.MagnetCatch:
                Debug.Log("마그넷 캐치 실행");
                //SkillFactory.Instance.GetMagnetCatch();
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
