using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISkillAffected
{
    void OnSkillAffect(SkillName skillName);
    void FinishSkillAffect(SkillName skillName);

}
