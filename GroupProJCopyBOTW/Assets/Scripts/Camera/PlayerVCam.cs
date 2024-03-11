using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVCam : MonoBehaviour
{
    public float rotateSpeed = 0.5f;

    CinemachineVirtualCamera vCam;
    Vector2 currMousePos;
    Vector2 preMousePos;
    Transform cameraRoot; 

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
        }
        else
        {
            Debug.LogWarning("Player가 없습니다.");
        }
    }

    private void LateUpdate()
    {
        preMousePos = currMousePos;
        currMousePos = Mouse.current.position.value;
        Vector2 dir = preMousePos - currMousePos;
        dir = dir.normalized;
        Debug.Log((dir.x * rotateSpeed * Vector3.up) + (dir.y * rotateSpeed * Vector3.right));
        cameraRoot.Rotate((dir.x * rotateSpeed * Vector3.up) + (dir.y * rotateSpeed * Vector3.right));
    }
}
