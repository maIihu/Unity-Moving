using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLevel : MonoBehaviour
{
    [SerializeField] private GameObject[] redGround;
    [SerializeField] private float activeTime;
    [SerializeField] private float inactiveTime;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;

    private void Start()
    {
        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
        StartCoroutine(HandleGroundLoop(redGround, activeTime, inactiveTime));    
    }

    private IEnumerator HandleGroundLoop(GameObject[] groundBlocks, float activeTime, float inactiveTime)
    {
        while (true)
        {
            foreach (var block in groundBlocks)
                SetObjectVisible(block, true);

            yield return new WaitForSeconds(activeTime);

            foreach (var block in groundBlocks)
                SetObjectVisible(block, false);

            yield return new WaitForSeconds(inactiveTime);
        }
    }
    
    private void SetObjectVisible(GameObject obj, bool isVisible)
    {
        if (!obj) return;

        var col = obj.GetComponent<Collider2D>();
        var render = obj.GetComponent<SpriteRenderer>();

        if (col) col.enabled = isVisible;
        if (render) render.enabled = isVisible;
    }
}
