using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MagnetCatch : MonoBehaviour, ISkill
{
    public float magnetDistance;
    public float playerRotateSpeed = 2.0f;
    public float targetSpeed = 3.0f;

    bool IsMagnetic => target != null;
    bool activatedSkill = false;

    IMagnetic target;
    Transform targetTransform;
    Transform targetOriginParent;
    Rigidbody targetRigid;
    Vector3 targetOffset;

    Player player;
    PlayerVCam playerVCam;

    readonly Vector3 Center = new Vector3(0.5f, 0.5f, 0.0f);

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
            //targetTransform.localPosition = targetOffset;
            //Debug.Log(targetTransform.parent.TransformDirection(targetTransform.localPosition) + " " + targetOffset);
            //Vector3 localPos = targetTransform.parent.InverseTransformDirection(targetTransform.position);
            targetRigid.MovePosition(targetTransform.parent.position + targetOffset);  // targetTransform.parent.position + 
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
                               
            }
        }
    }

    void Attach()
    {
        if (IsMagnetic)
        {
            targetOriginParent = targetTransform.parent;
            targetTransform.parent = player.transform.GetChild(1);
            targetOffset = targetTransform.localPosition;
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
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 vec = Vector3.zero;
        if (playerVCam != null)
        {
            vec = playerVCam.GetWorldPositionCenter();
        }
        Gizmos.DrawSphere(vec, 0.5f);

        Ray ray = Camera.main.ViewportPointToRay(Center);
        Gizmos.DrawRay(ray);
    }

#endif
}
