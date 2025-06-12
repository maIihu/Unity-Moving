using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    [SerializeField] private GameObject[] yellowGround;
    [SerializeField] private float timeToHandle;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;

    private void Start()
    {
        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main != null) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
        InvokeRepeating(nameof(HandleYellowGround), timeToHandle, timeToHandle * 2);
    }

    private void HandleYellowGround()
    {
        foreach (var item in yellowGround)
        {
            if(item.activeSelf) item.SetActive(false);
            else item.SetActive(true);
        }
    }
}
