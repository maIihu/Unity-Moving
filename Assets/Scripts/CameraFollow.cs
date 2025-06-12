using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    private Vector3 _offset ;
    private float _smoothSpeed;

    private void Awake()
    {
        _offset = new Vector3(0f, 0f, -10f);
        _smoothSpeed = 5f;
    }

    void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (!target) return;

        Vector3 desiredPosition = target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}