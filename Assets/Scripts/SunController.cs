using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SunController : MonoBehaviour
{
    [SerializeField] private int maxReflections = 5;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask mirrorLayer;
    [SerializeField] private LayerMask prismLayer;

    private void Start()
    {
        
    }

    private void Update()
    {
       // ReflectLight(transform.position, new Vector2(2, 3));
       Prism(transform.position, new Vector2(2, 3));
    }

    Vector2 Refract(Vector2 incoming, Vector2 normal, float n1, float n2)
    {
        float r = n1 / n2;
        float cosI = -Vector2.Dot(normal, incoming);
        float sinT2 = r * r * (1f - cosI * cosI);

        if (sinT2 > 1f)
        {
            return Vector2.Reflect(incoming, normal);
        }

        float cosT = Mathf.Sqrt(1f - sinT2);
        return r * incoming + (r * cosI - cosT) * normal;
    }

    
    private void Prism(Vector2 startPos, Vector2 direction)
    {
        Vector2 origin = startPos; // tia toi
        Vector2 dir = direction.normalized; // huong cua tia toi
        float epsilon = 0.01f; // do lech nho de tranh trung va cham

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, maxDistance, prismLayer);
            Debug.DrawLine(origin, hit.point, Color.yellow, 0f);

            float nAir = 1.0f;
            float nGlass = 1.5f;

            Vector2 normalIn = hit.normal;
            Vector2 refractedIn = Refract(dir, normalIn, nAir, nGlass);
            Vector2 entryPoint = hit.point + refractedIn * epsilon;

            // Raycast tìm mặt bên kia của lăng kính
            RaycastHit2D exitHit = Physics2D.Raycast(entryPoint, refractedIn, maxDistance, prismLayer);
            if (exitHit.collider)
            {
                Debug.DrawLine(hit.point, exitHit.point, Color.magenta);

                Vector2 normalOut = exitHit.normal;
                Vector2 refractedOut = Refract(refractedIn, normalOut, nGlass, nAir);
                dir = refractedOut;
                origin = exitHit.point + dir * epsilon;
            }
            // else
            // {
            //     // Nếu không tìm được mặt bên kia, vẽ tia tiếp tục trong kính
            //     Debug.DrawLine(hit.point, hit.point + refractedIn * maxDistance, Color.cyan);
            //     break;
            // }

        }
    }

    
    
    private void ReflectLight(Vector2 startPos, Vector2 direction)
    {
        Vector2 origin = startPos; // tia toi
        Vector2 dir = direction.normalized; // huong cua tia toi
        float epsilon = 0.01f; // do lech nho de tranh trung va cham

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, maxDistance, mirrorLayer);

            if (hit.collider)
            {
                Debug.DrawLine(origin, hit.point, Color.yellow, 0f);

                // tinh phan xa
                Vector2 normal = hit.normal.normalized;
                Vector2 incoming = dir;
                Vector2 reflected = incoming - 2 * Vector2.Dot(incoming, normal) * normal;

                // cap nhap tia tiep theo
                dir = reflected;
                origin = hit.point + dir * epsilon;
            }
            else
            {
                Debug.DrawLine(origin, origin + dir * maxDistance, Color.red, 0f);
                break;
            }
        }
    }

    
    #region Test

    private void ReflectAndDraw(Vector2 startPos, Vector2 direction)
    {
        float maxDistance = 100f;
        LayerMask mirrorLayer = LayerMask.GetMask("Mirror");

        // Raycast từ điểm bắt đầu
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction.normalized, maxDistance, mirrorLayer);

        if (hit.collider != null)
        {
            // Vẽ tia tới (màu vàng)
            Debug.DrawLine(startPos, hit.point, Color.yellow);

            // Lấy pháp tuyến tại điểm va chạm
            Vector2 normal = hit.normal.normalized;
            Vector2 incoming = direction.normalized;

            // Vẽ pháp tuyến (màu xanh lá)
            Debug.DrawRay(hit.point, normal * 0.5f, Color.green);

            // Tính phản xạ bằng công thức vật lý
            Vector2 reflected = incoming - 2 * Vector2.Dot(incoming, normal) * normal;

            // Vẽ tia phản xạ (màu xanh dương)
            Debug.DrawRay(hit.point, reflected * 5f, Color.cyan);

            // In góc tới
            float dot = Vector2.Dot(normal, incoming);
            dot = Mathf.Clamp(dot, -1f, 1f); // tránh lỗi do làm tròn

            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            // Luôn ép về góc nhỏ nhất
            if (angle > 90f) angle = 180f - angle;

            Debug.Log($"Góc tới: {angle} độ (vật lý)");

        }
        else
        {
            // Nếu không va chạm, vẽ tia tới cho hết tầm (màu đỏ)
            Debug.DrawLine(startPos, startPos + direction.normalized * maxDistance, Color.red);
        }
    }

    #endregion
}
