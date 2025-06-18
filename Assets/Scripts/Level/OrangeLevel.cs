using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeLevel : BaseLevel
{
    [Header("Block Yellow")]
    [SerializeField] private GameObject[] yellowGround;
    [SerializeField] private float yellowActiveTime;
    [SerializeField] private float yellowInactiveTime;

    [Header("Block Red")]
    [SerializeField] private GameObject[] redGround;
    [SerializeField] private float redActiveTime;
    [SerializeField] private float redInactiveTime;
    
    [Header("Red Laser")]
    [SerializeField] private GameObject[] redLaser;
    
    
    private void Start()
    {
        SpawnPlayer();
        
        StartCoroutine(HandleGroundLoop(yellowGround, yellowActiveTime, yellowInactiveTime));
        StartCoroutine(HandleGroundLoop(redGround, redActiveTime, redInactiveTime));
        StartCoroutine(HandleGroundLoop(redLaser, redActiveTime, redInactiveTime));
        
    }
    
    
}
