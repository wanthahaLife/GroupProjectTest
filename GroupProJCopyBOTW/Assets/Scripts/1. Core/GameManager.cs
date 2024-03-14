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

    SkillVCam skillCam;
    public SkillVCam SkillCam
    {
        get
        {
            if(skillCam == null)
            {
                skillCam = FindAnyObjectByType<SkillVCam>();
            }
            return skillCam;
        }
    }

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        skillCam = FindAnyObjectByType<SkillVCam>();
    }
}
