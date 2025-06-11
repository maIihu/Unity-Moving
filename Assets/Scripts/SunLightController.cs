using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLightController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private LayerMask prismLayer;

    private float n_air = 1.0f;
    private float n_glass = 1.5f;
    private void Update()
    {
        ReflectAndRefractLight(transform.position, transform.right); 
    }

    private void ReflectAndRefractLight(Vector2 startPos, Vector2 initialDirection)
    {
        Vector2 currentOrigin = startPos;
        Vector2 currentDir = initialDirection.normalized;
        float epsilon = 0.01f; 
        
        int maxBounces = 10; 
        float n1 = n_air;
        float n2 = n_glass;

        for (int i = 0; i < maxBounces; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentOrigin, currentDir, maxDistance);

            if (hit.collider)
            {
                Debug.DrawLine(currentOrigin, hit.point, Color.blue); 

                if (hit.collider.CompareTag("Mirror"))
                {
                    Vector2 normal = hit.normal;
                    currentDir = Vector2.Reflect(currentDir, normal); 
                    currentOrigin = hit.point + currentDir * epsilon; 
                }
                else if (hit.collider.CompareTag("Prism"))
                {
                    Debug.DrawLine(currentOrigin, hit.point, Color.Lerp(Color.red, Color.yellow, i * 0.1f));
                    Vector2 normal = hit.normal.normalized;

                    Vector2 refracted = Refract(currentDir, normal, n1, n2);

                    currentOrigin = hit.point + refracted * epsilon;
                    currentDir = refracted;

                    (n1, n2) = (n2, n1);
                }

            }
            else 
            {
                Debug.DrawLine(currentOrigin, currentOrigin + currentDir * maxDistance, Color.green); 
                break; 
            }
        }
    }
    Vector2 Refract(Vector2 incident, Vector2 normal, float n1, float n2)
    {
        float r = n1 / n2;
        float cosI = -Vector2.Dot(normal, incident);
        float sinT2 = r * r * (1 - cosI * cosI);
 
        if (sinT2 > 1f)
        {
            return Vector2.Reflect(incident, normal);
        }
 
        float cosT = Mathf.Sqrt(1 - sinT2);
        return r * incident + (r * cosI - cosT) * normal;
    }
}