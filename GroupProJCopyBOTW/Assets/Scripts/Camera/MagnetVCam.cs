using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetVCam : SkillVCam
{
    public void MagnetOnCamera()
    {
        VCam.Priority = 20;
    }
}
