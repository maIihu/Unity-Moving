using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    [SerializeField] private GameObject[] greenGround;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    private void Start()
    {
        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
    }
}
