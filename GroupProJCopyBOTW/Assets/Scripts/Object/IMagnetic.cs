using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagnetic : IMovable
{
    void OnStick();

    void Detach();
}
