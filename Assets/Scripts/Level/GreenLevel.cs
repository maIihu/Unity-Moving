using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLevel : BaseLevel
{
    [Header("Block")]
    [SerializeField] private GameObject[] greenGround;
    [SerializeField] private float activeTime;
    [SerializeField] private float inactiveTime;
    
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
    
    private IEnumerator SpawnBot()
    {
        yield return new WaitForSeconds(timeToSpawnBot);
        GameObject botClone = Instantiate(bot, spawnPoint.position, Quaternion.identity);
        botClone.GetComponent<BotInGreenMap>().player = _playerSpawn;
    }
    
}
