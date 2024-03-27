using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using System;
using UnityEngine.InputSystem;
using System.Security.Cryptography;





#if UNITY_EDITOR
using UnityEditor;
#endif

public class MagnetCatch : Skill
{
    public float magnetDistance;
    public float playerRotateSpeed = 2.0f;
    //public float moveXSpeed = 5.0f;
    public float targetMoveSpeed = 5.0f;
    //public float moveZSpeed = 1.0f;

    bool isMagnetic = false;

    Vector2 curMousePos = Vector2.zero;
    Vector2 preMousePos = Vector2.zero;

    Transform destinationX;

    Transform target;
    ReactionObject reactionTarget;
    Vector3 hitPoint;
    MagnetVCam magnetVcam;
    Cinemachine.CinemachineTargetGroup targetGroup;

    readonly Vector3 Center = new Vector3(0.5f, 0.5f, 0.0f);

    Action<Vector3> onStick;
    Action magnetCamOn;
    Action magnetCamOff;

    protected override void Awake()
    {
        base.Awake();
        destinationX = transform.GetChild(1);
        targetGroup = GetComponentInChildren<Cinemachine.CinemachineTargetGroup>();
        
    }

    
    protected override void Start()
    {
        base.Start();

        if(owner != null)
        {
            owner.onScroll += SetDestinationScroll;
        }
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if(magnetVcam == null)
        {
            magnetVcam = GameManager.Instance.Cam.MagnetCam;
        }

        magnetCamOn = magnetVcam.OnSkillCamera;
        magnetCamOff = magnetVcam.OffSkillCamera;

        isActivate = false;
        isMagnetic = false;
        targetGroup.m_Targets[1].target = owner.transform;

        StartCoroutine(TargetCheck());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
        onStick = null;
        reactionTarget = null;
        target = null;
    }

    private void FixedUpdate()
    {
        if(isActivate)
        {
            TargetPosition();
        }
    }
    void TargetPosition()
    {
        //float dirX = Camera.main.ViewportToWorldPoint(Center).x - target.position.x * moveXSpeed;
        Vector3 destDir = (destinationX.position - target.position).normalized;

        preMousePos = curMousePos;
        curMousePos = Mouse.current.position.value;
        Vector2 mouseDir = (curMousePos - preMousePos).normalized;
        
        Vector3 dir = new Vector3(destDir.x, mouseDir.y, destDir.z);

        onStick?.Invoke(dir * Time.fixedDeltaTime * targetMoveSpeed);
        // targetRigid.MovePosition(targetRigid.position + dir * Time.fixedDeltaTime);
    }

    protected override void OnSKillAction()
    {
        base.OnSKillAction();
        
    }
    protected override void UseSkillAction()
    {
        StopAllCoroutines();
        Debug.Log(isMagnetic);
        if (isMagnetic)
        {
            base.UseSkillAction();
            magnetCamOn?.Invoke();
            onStick = reactionTarget.AttachMagnetMove;
            reactionTarget.AttachMagnet();

            destinationX.position = hitPoint;
            destinationX.parent = owner.transform.GetChild(1);      // 플레이어의 CameraRoot를 부모로 설정해서 카메라와 동일한 움직임

            targetGroup.m_Targets[0].target = target;

            curMousePos = Mouse.current.position.value;
        }
    }

    protected override void OffSKillAction()
    {
        magnetCamOff?.Invoke();
        if (reactionTarget != null)
        {
            reactionTarget.DettachMagnet();
        }

        onStick = null;

        reactionTarget = null;
        target = null;
        isActivate = false;

        destinationX.parent = transform;

        base.OffSKillAction();
    }

    void SetDestinationScroll(float scrollY)
    {
        Vector3 pos = destinationX.localPosition;
        pos.z += scrollY;
        destinationX.localPosition = pos;
    }

    IEnumerator TargetCheck()
    {
        while (true) {
        Ray ray = Camera.main.ViewportPointToRay(Center);
            Physics.Raycast(ray, out RaycastHit hit, magnetDistance);
            target = hit.transform;
            if (target != null)
            {
                reactionTarget = target.GetComponent<ReactionObject>();
                isMagnetic = (reactionTarget != null) && (reactionTarget.Type == ReactionType.Magnetic);
                if (isMagnetic)
                {
                    hitPoint = hit.collider.bounds.center;
                    //onStick = reactionTarget.StickMagnetMove;
                }
            }

            yield return null;
        }
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
        OnSkill();
    }
    public void TestUseSkill()
    {
        UseSkill();
    }
    public void TestFinishSkill()
    {
        OffSkill();
    }
#endif
}
