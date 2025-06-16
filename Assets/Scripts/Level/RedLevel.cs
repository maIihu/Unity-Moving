using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLevel : MonoBehaviour
{
    [SerializeField] private GameObject[] redGround;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;

    private void Start()
    {
        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
        StartCoroutine(DestroyRedGround());
    }

    private IEnumerator DestroyRedGround()
    {
        yield return new WaitForSeconds(timeToDestroy);
        foreach (var item in redGround) ToggleObject(item);
    }
    
    private void ToggleObject(GameObject obj)
    {
        if (obj == null) return;

        var col = obj.GetComponent<Collider2D>();
        var render = obj.GetComponent<SpriteRenderer>();

        if (col != null) col.enabled = !col.enabled;
        if (render != null) render.enabled = !render.enabled;
    }
}
