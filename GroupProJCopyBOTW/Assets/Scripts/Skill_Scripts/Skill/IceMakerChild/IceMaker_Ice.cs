using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMaker_Ice : MonoBehaviour
{
    Animator animator;
    float generateTime = 1.0f;
    public float GenerateTime
    {
        set
        {
            if (generateTime != value)
            {
                generateTime = value;
                animator.SetFloat(Hash_GenerateSpeed, generateTime);
            }
        }
    }

    readonly int Hash_Generate = Animator.StringToHash("Generate");
    readonly int Hash_Destroy = Animator.StringToHash("Destroy");
    readonly int Hash_GenerateSpeed = Animator.StringToHash("GenerateSpeed");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat(Hash_GenerateSpeed, generateTime);
    }

    public void SetGenerate()
    {
        animator.SetTrigger(Hash_Generate);
    }
    public void SetDestroy()
    {
        animator.SetTrigger(Hash_Destroy);
    }
}
