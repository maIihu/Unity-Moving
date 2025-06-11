using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrism : MonoBehaviour
{
    [Header("Thiết lập ánh sáng")]
    Transform lightSource;

    public Vector2 lightDir;
    public LayerMask prismLayer;
 
    [Header("Chiết suất")]
    public float n_air = 1.0f;
    public float n_glass = 1.5f;
 
    [Header("Hiển thị")]
    public Color incidentColor = Color.white;
    public Color refractedColor = Color.cyan;

    private void Start()
    {
        lightSource = transform;
    }

    void Update()
    {
        Ham(lightSource.position, lightDir);
    }

    private void Ham(Vector2 startPos, Vector2 initialDirection)
    {
        Vector2 origin = startPos;
        Vector2 direction = initialDirection.normalized;
        float epsilon = 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 1000, prismLayer);
        if (hit.collider)
        {
            Debug.DrawLine(origin, hit.point, incidentColor);
            Vector2 normal = hit.normal.normalized;
            Vector2 incoming = direction;
            
            Vector2 refractedIn = Refract(incoming, normal, n_air, n_glass);
            Vector2 pointIn = hit.point + refractedIn * epsilon;

            RaycastHit2D hit2 = Physics2D.Raycast(pointIn, refractedIn, 1000, prismLayer);
            if (hit2.collider)
            {
                Debug.DrawLine(hit.point, hit2.point, Color.green);

                Vector2 normalOut = hit2.normal.normalized;
                
                Vector2 refractedOut = Refract(refractedIn, normalOut, n_glass, n_air);
                Debug.DrawRay(hit2.point, refractedOut * 5f, refractedColor);
            }
            else
            {
                Debug.DrawRay(pointIn, refractedIn * 5f, Color.magenta);

            }
        }
        else
        {
            Debug.DrawLine(origin, origin + direction * 1000, incidentColor);
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
