using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SunController : MonoBehaviour
{   
    [SerializeField] private float maxLightDistance = 5f;
    [SerializeField] private GameObject rayPointPrefab;
    
    private float _lightDistance;
    private Vector2 _startDirection;
    private Dictionary<GameObject, Vector2> _lightOfTheSun; // vat va cham - diem toi

    private void Start()
    {
        _lightDistance = maxLightDistance;
        _startDirection = new Vector2(2, 3);
        
        _lightOfTheSun = new Dictionary<GameObject, Vector2> { { this.gameObject, _startDirection } };
    }
    
    private void Update()
    {
        LightToMirror();

        foreach (var light in _lightOfTheSun)
        {
            Vector3 startPoint = light.Key.transform.position;
            Vector3 endPoint = startPoint + (Vector3)(light.Value.normalized * maxLightDistance);

            Debug.DrawLine(startPoint, endPoint, Color.cyan);
        }
    }


    private void LightToMirror()
    {
        var lastEntry = _lightOfTheSun.Last();

        Vector3 origin = lastEntry.Key.transform.position;
        Vector2 direction = lastEntry.Value.normalized;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxLightDistance);

        if (hit.collider && hit.collider.CompareTag("Mirror"))
        {
            Vector2 hitPoint = hit.point;               // điểm va chạm
            Vector2 incomingDir = direction;             // hướng tới (không đổi)
            Vector2 normal = hit.normal;

            Vector2 reflectDir = Vector2.Reflect(incomingDir, normal);

            float traveledDistance = Vector2.Distance(origin, hitPoint);
            float reflectDistance = maxLightDistance - traveledDistance;

            _lightOfTheSun[hit.collider.gameObject] = reflectDir;

        }
        else
        {
            // Nếu không trúng, xóa hết ngoại trừ SunController
            if (_lightOfTheSun.Count > 1)
            {
                var firstKey = _lightOfTheSun.First().Key;
                var keysToRemove = _lightOfTheSun.Keys.Where(k => k != firstKey).ToList();
                foreach (var key in keysToRemove)
                {
                    _lightOfTheSun.Remove(key);
                }
            }
        }
    }

    
    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawRay(transform.position, _direction * _lightDistance);
    }
}
