using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeLevel : MonoBehaviour
{
    [Header("Block Yellow")]
    [SerializeField] private GameObject[] yellowGround;
    [SerializeField] private float yellowActiveTime;
    [SerializeField] private float yellowInactiveTime;
    [SerializeField] private float yellowInitialDelay = 0.3f;

    [Header("Block Red")]
    [SerializeField] private GameObject[] redGround;
    [SerializeField] private float redActiveTime;
    [SerializeField] private float redInactiveTime;
    [SerializeField] private float redInitialDelay = 0.2f;
    
    [Header("Red Laser")]
    [SerializeField] private GameObject[] redLaser;
    
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    
    private void Start()
    {

        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main != null) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
        
        InvokeRepeating(nameof(HandleYellowGround), yellowInactiveTime, yellowInactiveTime * 2);
        StartCoroutine(HandleGroundLoop(redGround, redActiveTime, redInactiveTime));
        StartCoroutine(HandleGroundLoop(redLaser, redActiveTime, redInactiveTime));
        
    }
    
    private void HandleYellowGround()
    {
        foreach (var item in yellowGround)
        {
            if(item.activeSelf) item.SetActive(false);
            else item.SetActive(true);
        }
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
