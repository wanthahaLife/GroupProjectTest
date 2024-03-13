using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MagnetCatch : MonoBehaviour, ISkill
{
    public float magnetDistance;
    public float playerRotateSpeed = 2.0f;
    public float targetSpeed = 3.0f;

    bool IsMagnetic => target != null;
    bool activatedSkill = false;

    Transform destinationX;

    IMagnetic target;
    Transform targetTransform;
    Transform targetOriginParent;
    Rigidbody targetRigid;
    Vector3 targetStartLocalPosition;
    Vector3 hitPoint;

    Player player;
    PlayerVCam playerVCam;

    readonly Vector3 Center = new Vector3(0.5f, 0.5f, 0.0f);

    private void Awake()
    {
        destinationX = transform.GetChild(0);
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
        if(player != null)
        {
            player.onSkill += OnSkill;
            player.activatedSkill += Attach;
            player.inactivatedSkill += Detach;
        }
        playerVCam = GameManager.Instance.PlayerVCam;
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


    public void OnSkill()
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

    void Attach()
    {
        if (IsMagnetic && !activatedSkill)
        {
            destinationX.position = hitPoint;
            destinationX.parent = player.transform.GetChild(1);


            targetOriginParent = targetTransform.parent;
            targetTransform.parent = destinationX;
            targetRigid = targetTransform.GetComponent<Rigidbody>();
            target.Attach();

            activatedSkill = true;
        }
    }

    void Detach()
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
