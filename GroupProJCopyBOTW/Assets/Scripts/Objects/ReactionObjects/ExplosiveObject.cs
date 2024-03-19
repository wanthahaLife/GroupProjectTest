using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : DestructibleObject
{
    public float force = 4.0f;
    public float forceY = 5.0f;
    public float boomRange = 3.0f;
    public float damage = 3.0f;

    const float MaxDestroyDamage = 1000.0f;

    protected override void ObjectDestroy()
    {
        Boom();
        base.ObjectDestroy();
    }

    protected void Boom()
    {
        Debug.Log("폭발");
        Collider[] objects = Physics.OverlapSphere(transform.position, boomRange);
        foreach (Collider obj in objects)
        {
            DestructibleObject destructibleObj = obj.GetComponent<DestructibleObject>();
            if (destructibleObj != null)
            {
                destructibleObj.HitReaction(MaxDestroyDamage);
                continue;
            }
            MovableObject movableObj = obj.GetComponent<MovableObject>();
            if(movableObj != null)
            {
                Vector3 dir = obj.transform.position - transform.position;
                Vector3 power = dir.normalized * force + obj.transform.up * forceY;
                movableObj.ExplosionReaction(power);
            }

        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, boomRange);
    }
#endif
}
