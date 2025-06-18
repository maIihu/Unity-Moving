using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleLevel : BaseLevel
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
    
    private void Start()
    {
        SpawnPlayer();
        StartCoroutine(DelayedGroundLoop(blueGround, blueActiveTime, blueInactiveTime, blueInitialDelay));
        StartCoroutine(DelayedGroundLoop(greenGround, greenActiveTime, greenInactiveTime, greenInitialDelay));
    }
    
    private IEnumerator DelayedGroundLoop(GameObject[] groundBlocks, float activeTime, float inactiveTime, float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(HandleGroundLoop(groundBlocks, activeTime, inactiveTime));
    }

    
}
