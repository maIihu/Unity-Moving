using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLevel : MonoBehaviour
{
    [Header("Blue Block")] [SerializeField]
    private GameObject[] blueGround;
    [SerializeField] private float activeTime;
    [SerializeField] private float inactiveTime;
    
    [Header("Player")] 
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    
    [Header("Arrow")] 
    [SerializeField] private GameObject[] arrowPoint;
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject arrowPrefab;
    
    private void Start()
    {
        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
        StartCoroutine(HandleGroundLoop(blueGround, activeTime, inactiveTime));
        StartCoroutine(SpawnArrow());
    }

    private IEnumerator HandleGroundLoop(GameObject[] groundBlocks, float activeTime, float inactiveTime)
    {
        while (true)
        {
            foreach (var block in groundBlocks)
            {
                SetObjectVisible(block, true);
            }

            yield return new WaitForSeconds(activeTime);

            foreach (var block in groundBlocks)
            {
                SetObjectVisible(block, false);
            }

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

    private IEnumerator SpawnArrow()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            foreach (var arrow in arrowPoint)
            {
                Instantiate(arrowPrefab, arrow.transform.position, Quaternion.identity);
            }
        }
    }

}
