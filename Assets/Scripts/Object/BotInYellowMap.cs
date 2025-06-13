using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInYellowMap : MonoBehaviour
{
    [Header("Point Check")]
    [SerializeField] private Transform aheadCheck;

    [Header("Stat")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float checkDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D _rb;
    private bool _isMovingRight;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        GroundCheck();
    }

    private void Move()
    {
        Vector2 velocity = _rb.velocity;
        velocity.x = _isMovingRight ? moveSpeed : -moveSpeed;
        _rb.velocity = velocity;
        
        if (velocity.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    
    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(aheadCheck.position, aheadCheck.right, checkDistance, groundLayer);
        if (hit.collider)
        {
            _isMovingRight = !_isMovingRight;
        }
        Debug.DrawRay(aheadCheck.position, aheadCheck.right, Color.red);
    }
}
