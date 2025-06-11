using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SunLightController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 50f;
    
    private float n_air = 1.0f;
    private float n_glass = 1.5f;

    private LineRenderer _lineRenderer;
    

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
        _lineRenderer.widthMultiplier = 0.08f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("hge");
            ReflectAndRefractLight(transform.position, transform.right);
        }
    }

    private void ReflectAndRefractLight(Vector2 startPos, Vector2 initialDirection)
    {
        List<Vector3> lightPoints = new List<Vector3>(); 
        Vector2 currentOrigin = startPos;
        Vector2 currentDir = initialDirection.normalized;
        float epsilon = 0.01f;

        int maxBounces = 10;
        float n1 = n_air;
        float n2 = n_glass;

        lightPoints.Add(currentOrigin); 

        for (int i = 0; i < maxBounces; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentOrigin, currentDir, maxDistance);

            if (hit.collider)
            {
                lightPoints.Add(hit.point); 

                if (hit.collider.CompareTag("Mirror"))
                {
                    Vector2 normal = hit.normal;
                    currentDir = Vector2.Reflect(currentDir, normal);
                    currentOrigin = hit.point + currentDir * epsilon;
                }
                else if (hit.collider.CompareTag("Prism"))
                {
                    Vector2 normal = hit.normal.normalized;
                    Vector2 refracted = Refract(currentDir, normal, n1, n2);

                    currentOrigin = hit.point + refracted * epsilon;
                    currentDir = refracted;

                    (n1, n2) = (n2, n1);
                }
                else
                {
                    break; 
                }
            }
            else
            {
                lightPoints.Add(currentOrigin + currentDir * maxDistance); 
                break;
            }
        }

        _lineRenderer.positionCount = lightPoints.Count;
        for (int i = 0; i < lightPoints.Count; i++)
        {
            _lineRenderer.SetPosition(i, lightPoints[i]);
        }
    }

    private Vector2 Refract(Vector2 incident, Vector2 normal, float n1, float n2)
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
