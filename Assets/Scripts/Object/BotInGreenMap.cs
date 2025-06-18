using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInGreenMap : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (!player) return;
        Vector2 direction = (player.position - transform.position).normalized;
        _rb.velocity = direction * moveSpeed;
    }
    
}