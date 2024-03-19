using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBomb : Skill
{




    Rigidbody rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

    }

    protected override void StartSkillAction()
    {
        base.StartSkillAction();
    }

    protected override void UseSkillAction()
    {
        
    }

    void RemoteOn()
    {
        

        Boom();
    }

    void Boom()
    {
        gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    public void TestSkill()
    {
        StartSkillAction();
    }

    public void TestOnSkill()
    {
        UseSkillAction();
    }

    public void TestRemoteOn()
    {
        RemoteOn();
    }



#endif
}
