using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeLevel : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;

    private void Start()
    {
        Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
    }
}
