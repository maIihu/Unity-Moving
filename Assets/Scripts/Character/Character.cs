using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Character : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.2f;
    
    [Header("Jump Buffer")]
    [SerializeField] private float jumpBufferTime = 0.2f;

    private float _jumpBufferCounter;
    private float _coyoteTimeCounter;
    
    private Rigidbody2D _rb;
    private Animator _anim;
    private GameObject _groundCheck;
    
    private float _moveInput;
    private bool _isFacingRight;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _groundCheck = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        _isFacingRight = true;
    }

    private void Update()
    {
        _moveInput = Input.GetAxis("Horizontal");

        if (IsGrounded())
        {
            _coyoteTimeCounter = coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _jumpBufferCounter = 0;
        }

        if (Input.GetButtonUp("Jump") && _rb.velocity.y > 0f)
        {            
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.8f);
            _coyoteTimeCounter = 0;
        }
        
        if ((_isFacingRight && _moveInput < 0) || (!_isFacingRight && _moveInput > 0))
        {
            Flip();
        }
        
        UpdateAnimation();
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void UpdateAnimation()
    {
        float speed = Mathf.Abs(_moveInput); 
        bool isGrounded = IsGrounded();
        float yVelocity = _rb.velocity.y;

        _anim.SetFloat("Speed", speed);
        _anim.SetBool("IsGrounded", isGrounded);
        _anim.SetFloat("VerticalVelocity", yVelocity);
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_moveInput * moveSpeed, _rb.velocity.y);
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.transform.position, groundCheckRadius, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(ContainString.BorderTag) ||
             other.gameObject.CompareTag(ContainString.TrapTag))
            GameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(ContainString.ArrowTag)  || 
            other.gameObject.CompareTag(ContainString.LaserTag) ||
            other.gameObject.CompareTag(ContainString.BotTag)) GameOver();
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDrawGizmos()
    {
        if (_groundCheck == null)
        {
            _groundCheck = transform.GetChild(0).gameObject;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.transform.position, groundCheckRadius);
    }
    
}
