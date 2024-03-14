using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBomb : Skill
{

    public float throwPower = 5.0f;
    public float force = 4.0f;
    public float forceY = 5.0f;
    public float boomRange = 1.2f;

    bool carry = false;


    Rigidbody rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        carry = false;

    }

    protected override void StartSkill()
    {
        base.StartSkill();
        if (!carry)
        {
            carry = true;
            rigid.isKinematic = true;
        }
    }

    protected override void UseSkill()
    {
        if (carry)
        {
            rigid.isKinematic = false;
            rigid.AddRelativeForce((transform.forward + transform.up) * throwPower, ForceMode.Impulse);
            carry = false;
        }
    }

    void RemoteOn()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, boomRange);
        foreach(Collider obj in objects)
        {
            SkillReactionObj skillReactionObj = obj.GetComponent<SkillReactionObj>();
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
        StartSkill();
    }

    public void TestOnSkill()
    {
        UseSkill();
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
