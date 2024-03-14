using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillVCam : PlayerVCam
{
    //public Vector3 skillCameraOffset = new Vector3(-0.8f, 1.2f, 0.0f);
    //bool useSkill = false;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        currMousePos = Mouse.current.position.value;
        preMousePos = currMousePos;
    }

    protected override void Start()
    {
        base.Start();
        if (player != null)
        {
            player.SkillController.onSkill += OnSkillCamera;
            //player.activatedSkill += OriginCamera;
            player.SkillController.inactivatedSkill += OriginCamera;
        }
        else
        {
            Debug.LogWarning("Player가 없습니다.");
        }
    }

    private void Update()
    {
        /*if (useSkill)
        {
            Vector3 offset = skillCameraOffset - personFollow.ShoulderOffset;
            if ((offset).sqrMagnitude > 0.01f)
            {
                personFollow.ShoulderOffset += skillCameraSpeed * Time.deltaTime * offset.normalized;
            }
        }
        else
        {
            Vector3 offset = originCameraOffset - personFollow.ShoulderOffset;
            if ((offset).sqrMagnitude > 0.01f)
            {
                personFollow.ShoulderOffset += skillCameraSpeed * Time.deltaTime * offset.normalized;
            }
        }*/
    }

    void OnSkillCamera()
    {
        vCam.Priority = 20;
        //useSkill = true;
    }

    void OriginCamera()
    {
        vCam.Priority = 1;
        //useSkill = false;
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
