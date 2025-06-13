using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLevel : MonoBehaviour
{
    [SerializeField] private GameObject[] blueGround;
    [SerializeField] private float activeTime;
    [SerializeField] private float inactiveTime;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    
    private void Start()
    {
        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main != null) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
        StartCoroutine(HandleGroundLoop(blueGround, activeTime, inactiveTime));
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
