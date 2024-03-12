using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player
    {
        get
        {
            if(player == null)
                player = FindAnyObjectByType<Player>();
            return player;
        }
    }

    PlayerVCam playerVCam;
    public PlayerVCam PlayerVCam
    {
        get
        {
            if(playerVCam == null)
            {
                playerVCam = FindAnyObjectByType<PlayerVCam>();
            }
            return playerVCam;
        }
    }

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        playerVCam = FindAnyObjectByType<PlayerVCam>();
    }
}
