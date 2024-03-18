using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBomb : Skill
{

    public float force = 4.0f;
    public float forceY = 5.0f;
    public float boomRange = 1.2f;



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
        Collider[] objects = Physics.OverlapSphere(transform.position, boomRange);
        foreach(Collider obj in objects)
        {
            SkillReactionObject skillReactionObj = obj.GetComponent<SkillReactionObject>();
            if(skillReactionObj != null)
            {
                Vector3 dir = obj.transform.position - transform.position;
                Vector3 power = dir.normalized * force + obj.transform.up * forceY;
                skillReactionObj.OnSkillAffect(skillName, power);
            }
        }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, boomRange);
    }

#endif
}
