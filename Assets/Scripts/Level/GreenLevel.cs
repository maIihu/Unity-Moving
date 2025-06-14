using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLevel : MonoBehaviour
{
    [Header("Block")]
    [SerializeField] private GameObject[] greenGround;
    [SerializeField] private float activeTime;
    [SerializeField] private float inactiveTime;
    
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    
    [Header("Bot")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bot;
    [SerializeField] private float timeToSpawnBot;

    private Transform _playerSpawn;
    
    private void Start()
    {
        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
        _playerSpawn = playerSpawn.transform;
        
        StartCoroutine(HandleGroundLoop(greenGround, activeTime, inactiveTime));
        StartCoroutine(SpawnBot());
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
        if (obj == null) return;

        var col = obj.GetComponent<Collider2D>();
        var render = obj.GetComponent<SpriteRenderer>();

        if (col != null) col.enabled = isVisible;
        if (render != null) render.enabled = isVisible;
    }
    
    private IEnumerator SpawnBot()
    {
        yield return new WaitForSeconds(timeToSpawnBot);
        GameObject botClone = Instantiate(bot, spawnPoint.position, Quaternion.identity);
        botClone.GetComponent<BotInGreenMap>().player = _playerSpawn;
    }
    
}
