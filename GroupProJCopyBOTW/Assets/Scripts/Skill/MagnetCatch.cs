using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagnetCatch : MonoBehaviour, ISkill
{
    public float magnetDistance;
    Vector3 center = new Vector3(0.5f, 0.5f, 0.0f);

    public void OnSkill()
    {
        Ray ray = Camera.main.ViewportPointToRay(center);
        Physics.Raycast(ray, out RaycastHit hit, magnetDistance);
        IMagnetic magneticObj = hit.transform.GetComponent<IMagnetic>();
        Debug.Log(Camera.main.ViewportToWorldPoint(center));
        
    }

    void CatchObject()
    {

    }
}
