using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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

    bool IsMagnetic => target != null;
    bool activatedSkill = false;

    /// <summary>
    /// 
    /// </summary>
    Transform destinationX;

    IMagnetic target;
    Transform targetTransform;
    Transform targetOriginParent;
    Rigidbody targetRigid;
    Vector3 hitPoint;

    readonly Vector3 Center = new Vector3(0.5f, 0.5f, 0.0f);
    readonly Vector3 Top = new Vector3(0f, 1.0f, 0f);


    private void Awake()
    {
        destinationX = transform.GetChild(0);
    }

    protected override void Start()
    {
        owner = GameManager.Instance.Player;
        if(owner != null)
        {
            owner.SkillController.onSkill += UseSkill;
            owner.SkillController.activatedSkill += StartSkill;
            owner.SkillController.inactivatedSkill += EndSkill;
        }
    }

    private void FixedUpdate()
    {
        if(activatedSkill)
        {
            /*float dirX = destinationX.position.x - targetRigid.position.x;
            if(dirX.sqrMagnitude > 0.01f)
            {
                //Debug.Log(destination.position + " " + targetRigid.position);
                targetRigid.MovePosition(targetRigid.position + dirX.normalized * Time.fixedDeltaTime * targetSpeed);
            }*/
            /*Vector3 pos = Camera.main.ViewportToWorldPoint(Center);
            pos.z = targetTransform.position.z + 5;
            targetTransform.position = pos;*/
        }
    }



    protected override void StartSkill()
    {
        if (IsMagnetic && !activatedSkill)
        {
            destinationX.position = hitPoint;
            destinationX.parent = owner.transform.GetChild(1);


            targetOriginParent = targetTransform.parent;
            targetTransform.parent = destinationX;
            targetRigid = targetTransform.GetComponent<Rigidbody>();
            target.Attach();

            activatedSkill = true;
        }
    }
    protected override void UseSkill()
    {
        Ray ray = Camera.main.ViewportPointToRay(Center);
        Physics.Raycast(ray, out RaycastHit hit, magnetDistance);
        targetTransform = hit.transform;
        if (targetTransform != null)
        {
            target = targetTransform.GetComponent<IMagnetic>();
            if (IsMagnetic)
            {
                hitPoint = hit.point;
            }
        }
    }

    protected override void EndSkill()
    {
        if (activatedSkill)
        {
            target.Detach();
            target = null;
            activatedSkill = false;
            targetTransform.parent = targetOriginParent;

            destinationX.parent = transform;
        }
    }

    void Move()
    {
        float x = (destinationX.position - targetTransform.position).normalized.x;
        float y = Time.fixedDeltaTime * moveYSpeed;
    }

    float DirectionX()
    {
        float dir = destinationX.position.x;
        return dir;
    }

    float DirectionY()
    {
        float dir = Camera.main.ViewportToWorldPoint(Center).y;
        return dir;
    }

    void MoveZ()
    {

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

#endif
}
