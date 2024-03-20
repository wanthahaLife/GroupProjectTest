using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRootTracker : MonoBehaviour
{
    public void OnTracking(Transform target)
    {
        StartCoroutine(Trakcking(target));
    }

    public void OffTracking()
    {
        StopAllCoroutines();
    }

    IEnumerator Trakcking(Transform target)
    {
        Vector3 pos = target.position;
        while (true)
        {
            transform.position = pos;
            yield return null;
        }
    }
}
