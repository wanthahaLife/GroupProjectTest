using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagnetCatch : MonoBehaviour, ISkill
{
    public float magnetDistance;
    public float playerRotateSpeed = 2.0f;
    public float targetSpeed = 3.0f;

    bool IsMagnetic => target != null;
    bool activatedSkill = false;
    Vector3 targetDistance = Vector3.zero;

    IMagnetic target;
    Transform targetTransform;
    Vector3 targetDir = Vector3.zero;

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

    private void Update()
    {
        if (activatedSkill)
        {
            targetDir = Camera.main.transform.position + targetDistance;
            Debug.Log(targetDir);
            targetTransform.position = targetDir;
        }
    }

    public void OnSkill()
    {
        Ray ray = Camera.main.ViewportPointToRay(Center);
        Physics.Raycast(ray, out RaycastHit hit, magnetDistance);

        targetTransform = hit.transform;
        if(targetTransform != null) 
            target = targetTransform.GetComponent<IMagnetic>();
        if (IsMagnetic)
        {
            targetDir = targetTransform.position;
            targetDistance = targetTransform.position - Camera.main.transform.position;
        }
    }

    void Attach()
    {
        activatedSkill = IsMagnetic;
    }

    void Detach()
    {
        target = null;
        activatedSkill = false;
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

        Gizmos.DrawSphere(targetDir, 0.5f);
    }

#endif
}
