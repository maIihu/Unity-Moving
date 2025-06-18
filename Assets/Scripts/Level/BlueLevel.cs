using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLevel : BaseLevel
{
    [Header("Blue Block")] 
    [SerializeField] private GameObject[] blueGround;
    [SerializeField] private float activeTime;
    [SerializeField] private float inactiveTime;
    
    [Header("Arrow")] 
    [SerializeField] private GameObject[] arrowPoint;
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject arrowPrefab;
    
    private void Start()
    {
        SpawnPlayer();
        StartCoroutine(HandleGroundLoop(blueGround, activeTime, inactiveTime));
        StartCoroutine(SpawnArrow());
    }
    
    private IEnumerator SpawnArrow()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            foreach (var arrow in arrowPoint)
            {
                Instantiate(arrowPrefab, arrow.transform.position, Quaternion.identity);
            }
        }
    }

}
