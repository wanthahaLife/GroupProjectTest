using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetVCam : SkillVCam
{
    protected override void UsingSkillCamera()
    {
        base.UsingSkillCamera();
        VCam.Priority = 20;
    }

    protected override void EndSkillCamera()
    {
        base.EndSkillCamera();
        VCam.Priority = 1;
    }
}
