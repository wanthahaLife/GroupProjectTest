using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBomb : Skill
{

    public float throwPower = 5.0f;
    public float force = 4.0f;
    public float forceY = 5.0f;
    public float boomRange = 1.2f;


    Rigidbody rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected override void StartSkill()
    {
        base.StartSkill();
        rigid.isKinematic = true;
        PlayerSkillController skillController = player.SkillController;
        transform.position = skillController.SkillRoot.position;
    }

    protected override void UseSkill()
    {
        rigid.isKinematic = false;
        rigid.AddRelativeForce((transform.forward + transform.up) * throwPower, ForceMode.Impulse);

    }

    void RemoteOn()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, boomRange);
        foreach(Collider obj in objects)
        {
            ISkillAffected destruct = obj.GetComponent<ISkillAffected>();
            IMovable movable = obj.GetComponent<IMovable>();
            if(destruct != null)
            {
                destruct.OnSkillAffect(skillName);
            }
            else if(movable != null)
            {
                Vector3 dir = obj.transform.position - transform.position;
                movable.MoveForce(dir.normalized * force + obj.transform.up * forceY);
            }
        }

        Boom();
    }

    void Boom()
    {

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
