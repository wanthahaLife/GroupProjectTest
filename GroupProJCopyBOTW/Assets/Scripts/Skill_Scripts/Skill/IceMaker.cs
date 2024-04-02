using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMaker : Skill
{
    Queue<Transform> usingIceQueue;
    Queue<Transform> readyIceQueue;

    Transform preview;

    protected override void Awake()
    {
        Transform ices = transform.GetChild(1);

        usingIceQueue = new Queue<Transform>(ices.childCount);
        readyIceQueue = new Queue<Transform>(ices.childCount);
        for(int i = 0; i < ices.childCount; i++)
        {
            readyIceQueue.Enqueue(ices.GetChild(i));
        }
        
        preview = transform.GetChild(2);
    }


}
