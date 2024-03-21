using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemoteBomb : Skill
{
    [SerializeField] ExplosiveObject explosiveInfo;

    public float Weight = 1.0f;
    float reducePower = 1.0f;

    enum StateType
    {
        None = 0,
        PickUp,
        Throw,
        Move,
        Destroy,
        Boom,
    }

    StateType currentState = StateType.None;
  
    Rigidbody rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        if (rigid == null)
        {
            rigid = transform.AddComponent<Rigidbody>();
        }
        reducePower = 1.0f / Weight;
    }

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

    public void ExplosionReaction()
    {
        if (currentState == StateType.None)
        {
            Boom();
        }
    }

    void Boom()
    {
        if (currentState != StateType.Boom)
        {
            currentState = StateType.Boom;
            // -- 폭발 동작 코루틴 추가해야됨
            Collider[] objects = Physics.OverlapSphere(transform.position, explosiveInfo.boomRange);
            foreach (Collider obj in objects)
            {
                // 밑에 수정
                ReactionObject reactionObj = obj.GetComponent<ReactionObject>();
                RemoteBomb remoteBomb = obj.GetComponent<RemoteBomb>();
                if (reactionObj != null)
                {
                    reactionObj.HitReaction(true);
                    Vector3 dir = obj.transform.position - transform.position;
                    Vector3 power = dir.normalized * explosiveInfo.force + obj.transform.up * explosiveInfo.forceY;
                    reactionObj.ExplosionReaction(power);
                }
                else if (remoteBomb != null)
                {
                    remoteBomb.ExplosionReaction();
                }
            }
        }

        // -- 폭발 애니메이션 코루틴 추가
        gameObject.SetActive(false);
    }

    public void PickUp(Transform root)
    {
        if (currentState == StateType.None)
        {
            transform.parent = root;
            Vector3 destPos = root.position;

            transform.position = destPos;
            currentState = StateType.PickUp;
            rigid.isKinematic = true;
        }
    }

    public void Throw(float throwPower, Transform user)
    {
        if (currentState == StateType.PickUp)
        {
            rigid.isKinematic = false;
            currentState = StateType.Throw;

            rigid.AddForce((user.forward + user.up) * throwPower * reducePower, ForceMode.Impulse);
            transform.parent = originParent;
        }
    }

    public void Drop()
    {
        if (currentState == StateType.PickUp)
        {
            transform.parent = originParent;
            //isCarried = true;
            currentState = StateType.None;
            rigid.isKinematic = false;
        }
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
