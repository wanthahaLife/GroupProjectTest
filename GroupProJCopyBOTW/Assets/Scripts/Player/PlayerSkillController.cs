using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    Transform handRootTracker;
    public Transform HandRoot => handRootTracker;
    Player player;

    public Action onSKillAction;
    public Action useSkillAction;
    public Action offSkillAction;

    SkillName currentSkill = SkillName.RemoteBomb;

    RemoteBomb remoteBomb;
    RemoteBomb remoteBombCube;
    MagnetCatch magnetCatch;

    private void Awake()
    {
        handRootTracker = transform.GetChild(3);
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

            player.onSkillSelect += ConnectSkill;

            player.onSkill += () => onSKillAction?.Invoke();
            player.onSkill += OnSkill;
            player.rightClick += () => useSkillAction?.Invoke();
            player.onCancel += () => offSkillAction?.Invoke();

            //onSKillAction += OnSkill;

        }
    }

    void ConnectSkill(SkillName skiilName)
    {
        // 구현중
        currentSkill = skiilName;
        switch (currentSkill)
        {
            case SkillName.RemoteBomb:
                Debug.Log("변경 : 리모컨 폭탄");
                if (remoteBomb != null)
                {
                    onSKillAction = remoteBomb.OnSkillAction;
                    useSkillAction = remoteBomb.UseSkillAction;
                    offSkillAction = remoteBomb.OffSkillAction;
                }

                break;
            case SkillName.RemoteBomb_Cube:
                Debug.Log("변경 : 리모컨 폭탄(큐브)");
                if (remoteBombCube != null)
                {
                    onSKillAction = remoteBombCube.OnSkillAction;
                    useSkillAction = remoteBombCube.UseSkillAction;
                    offSkillAction = remoteBombCube.OffSkillAction;
                }
                break;
            case SkillName.MagnetCatch:
                Debug.Log("변경 : 마그넷 캐치");
                if (magnetCatch != null)
                {
                    onSKillAction = magnetCatch.OnSkillAction;
                    useSkillAction = magnetCatch.UseSkillAction;
                    offSkillAction = magnetCatch.OffSkillAction;
                }
                break;
            case SkillName.IceMaker:
                break;
            case SkillName.TimeLock:
                break;
        }

    }

    void OnSkill()
    {
        switch(currentSkill)
        {
            case SkillName.RemoteBomb:
                Debug.Log("실행 : 리모컨 폭탄");
                if(remoteBomb == null)
                {
                    remoteBomb = SkillFactory.Instance.GetRemoteBomb();
                }
                
                break;
            case SkillName.RemoteBomb_Cube:
                Debug.Log("실행 : 리모컨 폭탄 큐브");
                if (remoteBombCube == null)
                {
                    remoteBombCube = SkillFactory.Instance.GetRemoteBomb();
                }
                break;
            case SkillName.MagnetCatch:
                Debug.Log("실행 : 마그넷 캐치");
                if (magnetCatch == null)
                {
                    magnetCatch = SkillFactory.Instance.GetMagnetCatch();
                }
                break;
            case SkillName.IceMaker:
                break;
            case SkillName.TimeLock: 
                break;
        }
        ConnectSkill(currentSkill);
        onSKillAction?.Invoke();
    }


#if UNITY_EDITOR

    void TestSkillSelect(SkillName skillName)
    {
        Debug.Log(skillName);
    }

#endif
}
