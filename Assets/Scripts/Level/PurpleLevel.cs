using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleLevel : MonoBehaviour
{
    [Header("Block Blue")]
    [SerializeField] private GameObject[] blueGround;
    [SerializeField] private float blueActiveTime;
    [SerializeField] private float blueInactiveTime;
    [SerializeField] private float blueInitialDelay = 0.3f;

    [Header("Block Green")]
    [SerializeField] private GameObject[] greenGround;
    [SerializeField] private float greenActiveTime;
    [SerializeField] private float greenInactiveTime;
    [SerializeField] private float greenInitialDelay = 0.2f;
    
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    
    private void Start()
    {
        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
        StartCoroutine(DelayedGroundLoop(blueGround, blueActiveTime, blueInactiveTime, blueInitialDelay));
        StartCoroutine(DelayedGroundLoop(greenGround, greenActiveTime, greenInactiveTime, greenInitialDelay));
    }
    
    private IEnumerator DelayedGroundLoop(GameObject[] groundBlocks, float activeTime, float inactiveTime, float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(HandleGroundLoop(groundBlocks, activeTime, inactiveTime));
    }

    
    private IEnumerator HandleGroundLoop(GameObject[] groundBlocks, float activeTime, float inactiveTime)
    {
        while (true)
        {
            foreach (var block in groundBlocks)
            {
                block.SetActive(true);
            }

            yield return new WaitForSeconds(activeTime);

            foreach (var block in groundBlocks)
            {
                block.SetActive(false);
            }

            yield return new WaitForSeconds(inactiveTime);
        }
    }
}
