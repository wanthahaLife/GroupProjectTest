using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using System;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class MagnetCatch : Skill
{
    public float magnetDistance;
    public float playerRotateSpeed = 2.0f;
    public float moveXSpeed = 5.0f;
    public float moveYSpeed = 2.0f;
    public float moveZSpeed = 1.0f;

    bool isMagnetic = false;
    bool activatedSkill = false;

    Vector3 cameraDir = Vector3.zero;

    Transform destinationX;

    Transform target;
    Vector3 hitPoint;
    PlayerVCam vcam;
    Cinemachine.CinemachineTargetGroup targetGroup;

    readonly Vector3 Center = new Vector3(0.5f, 0.5f, 0.0f);
    readonly Vector3 Top = new Vector3(0f, 1.0f, 0f);

    Action<Vector3> onStick;

    protected override void Awake()
    {
        base.Awake();
        destinationX = transform.GetChild(1);
        targetGroup = GetComponentInChildren<Cinemachine.CinemachineTargetGroup>();
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if(vcam == null)
        {
            vcam = GameManager.Instance.PlayerCam;
        }

    }

    private void FixedUpdate()
    {
        if(activatedSkill)
        {
            MoveTarget();
        }
    }

    public override void OnSkillAction()
    {
        base.OnSkillAction();
        StartCoroutine(TargetCheck());
    }
    public override void UseSkillAction()
    {
        base.UseSkillAction();
        StopAllCoroutines();
        if (isMagnetic && !activatedSkill)
        {
            destinationX.position = hitPoint;
            destinationX.parent = owner.transform.GetChild(1);      // 플레이어의 CameraRoot를 부모로 설정해서 카메라와 동일한 움직임

            targetGroup.m_Targets[0].target = target;

            activatedSkill = true;

        }
    }

    public override void OffSkillAction()
    {
        base.OffSkillAction();
        if (activatedSkill)
        {
            target = null;
            activatedSkill = false;

            destinationX.parent = transform;
        }
    }

    IEnumerator TargetCheck()
    {
        while (true) {
        Ray ray = Camera.main.ViewportPointToRay(Center);
            Physics.Raycast(ray, out RaycastHit hit, magnetDistance);
            target = hit.transform;
            if (target != null)
            {
                ReactionObject reactionTarget = target.GetComponent<ReactionObject>();
                isMagnetic = (reactionTarget != null) && (reactionTarget.Type == ReactionType.Magnetic);
                if (isMagnetic)
                {
                    hitPoint = hit.collider.bounds.center;
                    onStick = reactionTarget.StickMagnetMove;
                }
            }

            yield return null;
        }
    }

    void MoveTarget()
    {

        float dirX = Camera.main.ViewportToWorldPoint(Center).x - target.position.x * moveXSpeed;
        float dirY = cameraDir.y * moveYSpeed;
        float dirZ = 0f;

        Vector3 dir = new Vector3(dirX, dirY, dirZ);

        onStick?.Invoke(dir * Time.fixedDeltaTime);
        // targetRigid.MovePosition(targetRigid.position + dir * Time.fixedDeltaTime);
    }

    void CameraMove(Vector3 pos)
    {
        cameraDir = pos;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ViewportPointToRay(Center);
        Physics.Raycast(ray, out RaycastHit hit, magnetDistance);
        // 레이캐스트 보여주는 기즈모
        if(hit.transform != null)
        {
            Gizmos.color = Color.red;
            Vector3 vec = Camera.main.ViewportToWorldPoint(Center);
            Gizmos.DrawLine(vec, hit.point);
        }

    }

    public void TestStartSkill()
    {
        OnSkillAction();
    }
    public void TestUseSkill()
    {
        UseSkillAction();
    }
    public void TestFinishSkill()
    {
        OffSkillAction();
    }
#endif
}
