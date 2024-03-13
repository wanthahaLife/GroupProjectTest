using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    Transform skillRoot;
    public Transform SkillRoot => skillRoot;

    private void Awake()
    {
        skillRoot = GetComponentInChildren<SkillRoot>().transform;
    }
}
