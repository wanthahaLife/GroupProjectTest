using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVCam : MonoBehaviour
{
    /// <summary>
    /// 회전 속도
    /// </summary>
    public float rotateSpeed = 2.0f;
    public float cameraSpeed = 10.0f;
    public float maxAngle = 30.0f;
    public Vector3 skillCameraOffset = new Vector3(-2.0f, 1.5f, 0.0f);

    Vector3 originCameraOffset;

    float angleY = 0f;
    float angleX = 0f;

    bool useSkill = false;

    CinemachineVirtualCamera vCam;
    Vector2 currMousePos;
    Vector2 preMousePos;
    Transform cameraRoot;
    Cinemachine3rdPersonFollow personFollow;

    readonly Vector3 Center = new Vector3(0.5f, 0.5f, 0.0f);

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        currMousePos = Mouse.current.position.value;
        preMousePos = currMousePos;
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        if(player != null )
        {
            cameraRoot = player.transform.GetChild(0);
            vCam.Follow = cameraRoot;
            player.onSkill += OnSkillCamera;
            player.inactivatedSkill += OriginCamera;
        }
        else
        {
            Debug.LogWarning("Player가 없습니다.");
        }

        personFollow = vCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        originCameraOffset = personFollow.ShoulderOffset;
    }

    private void LateUpdate()
    {
        preMousePos = currMousePos;
        currMousePos = Mouse.current.position.value;
        Vector2 dir = currMousePos - preMousePos;
        dir = dir.normalized;
        angleY += dir.x * rotateSpeed;
        angleX -= dir.y * rotateSpeed;
        angleX = Mathf.Clamp(angleX, -maxAngle, maxAngle);

        Quaternion rotate = Quaternion.Euler(angleX, angleY, 0);
        cameraRoot.rotation = rotate;
    }

    private void Update()
    {
        if (useSkill)
        {
            Vector3 offset = skillCameraOffset - personFollow.ShoulderOffset;
            if ((offset).sqrMagnitude > 0.01f)
            {
                personFollow.ShoulderOffset += cameraSpeed * Time.deltaTime * offset.normalized;
            }
        }
        else
        {
            Vector3 offset = originCameraOffset - personFollow.ShoulderOffset;
            if ((offset).sqrMagnitude > 0.01f)
            {
                personFollow.ShoulderOffset += cameraSpeed * Time.deltaTime * offset.normalized;
            }
        }
    }

    void OnSkillCamera()
    {
        useSkill = true;
    }

    void OriginCamera()
    {
        useSkill = false;
    }

    public Vector3 GetWorldPositionCenter()
    {
        Vector3 screenPoint = Camera.main.ViewportToScreenPoint(Center);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPoint);
        return worldPosition;
    }

#if UNITY_EDITOR
    public void TestSkillCamera()
    {
        OnSkillCamera();
    }

    public void TestOriginCamera()
    {
        OriginCamera();
    }
#endif
}
