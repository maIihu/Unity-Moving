using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] private GameObject[] redGround;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;

    private void Start()
    {
        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
//        if (Camera.main != null) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
        StartCoroutine(DestroyRedGround());
    }

    private IEnumerator DestroyRedGround()
    {
        yield return new WaitForSeconds(timeToDestroy);
        foreach (var item in redGround) item.SetActive(false);
    }
}
