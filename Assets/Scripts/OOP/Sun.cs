using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float maxDistance = 100f;    
    private void Update()
    {
        RayCastOp(this.transform.position, Vector2.right);
    }

    private void RayCastOp(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);
        Debug.DrawLine(origin, origin + direction * maxDistance, Color.yellow, 0f);
        if (hit.collider)
        {
            if (hit.collider.TryGetComponent<IPhysicsable>(out var obj))
            {
                obj.TakeLight();
                
            }
        }
    }
    
}
