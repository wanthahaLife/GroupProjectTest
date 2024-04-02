using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HandRootTracker : MonoBehaviour
{
    Transform character;

    private void Awake()
    {
        Transform parent = transform.parent;
        character = parent.GetChild(0);
    }

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
        while (true)
        {
            transform.position = target.position;
            transform.rotation = character.transform.rotation;
            yield return null;
        }
    }
}
