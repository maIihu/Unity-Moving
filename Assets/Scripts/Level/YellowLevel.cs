using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLevel : BaseLevel
{
    [SerializeField] private GameObject[] yellowGround;
    [SerializeField] private float yellowActiveTime = 2;
    [SerializeField] private float yellowInactiveTime = 1;

    [SerializeField] private GameObject bot;

    private void Start()
    {
        SpawnPlayer();
        StartCoroutine(HandleGroundLoop(yellowGround, yellowActiveTime, yellowInactiveTime));
    }
    

}
