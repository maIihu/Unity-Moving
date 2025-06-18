using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLevel : BaseLevel
{
    [SerializeField] private GameObject[] redGround;
    [SerializeField] private float activeTime;
    [SerializeField] private float inactiveTime;
    
    private void Start()
    {
        SpawnPlayer();
        StartCoroutine(HandleGroundLoop(redGround, activeTime, inactiveTime));    
    }
    

    
    
}
