using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVCam : MonoBehaviour
{
    CinemachineVirtualCamera vCam;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        if(player != null )
        {
            vCam.Follow = player.transform.GetChild(0);
        }
        else
        {
            Debug.LogWarning("Player가 없습니다.");
        }
    }
}
