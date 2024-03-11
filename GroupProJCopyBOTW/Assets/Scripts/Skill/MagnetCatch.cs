using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagnetCatch : MonoBehaviour
{
    void OnMagnetChatch()
    {
        Ray ray = Camera.main.ScreenPointToRay(Vector3.forward);
        //Physics.Raycast()
    }
}
