using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemoteBomb : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        currentState = StateType.PickUp;
    }

    protected override void OnSkillAction()
    {
        if(currentState == StateType.None)
        {
            Boom();
        }
        else if(currentState == StateType.PickUp)
        {
            // 집어 넣기
            gameObject.SetActive(false);
        }
        Debug.Log("리모컨");
    }

    protected override void OffSkillAction()
    {
    }

    protected override void CollisionAfterThrow()
    {
        currentState = StateType.None;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosiveInfo.boomRange);

    }

    public void TestSkill()
    {
        OnSkillAction();
    }

    public void TestOnSkill()
    {
        UseSkillAction();
    }

    public void TestRemoteOn()
    {
        //RemoteOn();
    }



#endif
}
