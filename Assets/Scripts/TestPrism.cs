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
        Vector2 origin = lightSource.position;
        Vector2 direction = lightDir.normalized;
        float epsilon = 0.005f;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 1000, prismLayer);

        if (hit.collider)
        {
            // Tia tới màu trắng
            Debug.DrawLine(origin, hit.point, Color.white);

            // Tính vector khúc xạ vào lăng kính
            Vector2 normal = hit.normal.normalized;
            Vector2 refractedIn = Refract(direction, normal, n_air, n_glass);
            Vector2 pointIn = hit.point + refractedIn * epsilon;

            // Tia đi trong lăng kính chia thành 7 tia màu
            Color[] rainbowColors = new Color[]
            {
                Color.red,
                new Color(1f, 0.5f, 0f), // cam
                Color.yellow,
                Color.green,
                Color.blue,
                new Color(0.29f, 0f, 0.51f), // chàm
                Color.magenta // tím
            };

            float[] refractiveIndices = new float[]
            {
                1.50f, // đỏ
                1.505f, // cam
                1.51f, // vàng
                1.515f, // lục
                1.52f, // lam
                1.525f, // chàm
                1.53f  // tím
            };

            for (int i = 0; i < 7; i++)
            {
                HamRainbow(pointIn, refractedIn, refractiveIndices[i], rainbowColors[i]);
            }
        }
        else
        {
            // Nếu không va chạm, vẽ tia trắng duy nhất
            Debug.DrawLine(origin, origin + direction * 1000, Color.white);
        }
    }


    private void HamRainbow(Vector2 startPos, Vector2 initialDirection, float n_glass_i, Color rayColor)
    {
        Vector2 direction = initialDirection.normalized;

        RaycastHit2D hit2 = Physics2D.Raycast(startPos, direction, 1000, prismLayer);
        if (hit2.collider)
        {
            Debug.DrawLine(startPos, hit2.point, rayColor);
            Vector2 normalOut = hit2.normal.normalized;
            Vector2 refractedOut = Refract(direction, normalOut, n_glass_i, n_air);
            Debug.DrawRay(hit2.point, refractedOut * 5f, rayColor);
        }
        else
        {
            Debug.DrawRay(startPos, direction * 5f, rayColor);
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
