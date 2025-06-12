using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    private Vector3 _offset ;
    private float _smoothTime;
    private Vector3 _velocity;
    
    private void Awake()
    {
        _offset = new Vector3(0f, 0f, -10f);
        _smoothTime = 0.25f;
        _velocity = Vector3.zero;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (!target) return;

        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}