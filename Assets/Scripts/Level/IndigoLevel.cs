using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndigoLevel : MonoBehaviour
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
    
    [Header("Block Purple")]
    [SerializeField] private GameObject[] purpleGround;
    [SerializeField] private float purpleActiveTime;
    [SerializeField] private float purpleInactiveTime;
    [SerializeField] private float purpleInitialDelay = 0.5f;
    
    [Header("Arrow")]
    [SerializeField] private Transform[] arrowPoints;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float timeToSpawn; 
    [SerializeField] private float delayBetweenFirstAndRest  = 0.4f;

    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;

    [SerializeField] private Transform arrowContainer;
    
    private Queue<GameObject> _arrowPool;
    private int _poolSize = 32;

    private void Start()
    {
        _arrowPool = new Queue<GameObject>();
        CreateObjectPool();

        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main != null) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;

        StartCoroutine(DelayedGroundLoop(blueGround, blueActiveTime, blueInactiveTime, blueInitialDelay));
        StartCoroutine(DelayedGroundLoop(greenGround, greenActiveTime, greenInactiveTime, greenInitialDelay));
        StartCoroutine(DelayedGroundLoop(purpleGround, purpleActiveTime, purpleInactiveTime, purpleInitialDelay));
        
        StartCoroutine(SpawnArrowLoop());
    }

    private void CreateObjectPool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowContainer);
            arrow.SetActive(false);
            _arrowPool.Enqueue(arrow);
        }
    }

    private GameObject GetArrowFromPool()
    {
        if (_arrowPool.Count > 0)
        {
            GameObject arrow = _arrowPool.Dequeue();
            arrow.SetActive(true);
            return arrow;
        }

        GameObject newArrow = Instantiate(arrowPrefab, arrowContainer);
        return newArrow;
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
    
    private IEnumerator SpawnArrowLoop()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnArrowSequence());
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    private IEnumerator SpawnArrowSequence()
    {
        if (arrowPoints.Length == 0) yield break;
        GameObject arrow1 = GetArrowFromPool();
        arrow1.transform.position = arrowPoints[0].position;
        arrow1.transform.rotation = Quaternion.identity;

        yield return new WaitForSeconds(delayBetweenFirstAndRest);

        for (int i = 1; i < arrowPoints.Length; i++)
        {
            GameObject arrow = GetArrowFromPool();
            arrow.transform.position = arrowPoints[i].position;
            arrow.transform.rotation = Quaternion.identity;
        }
    }


    public void ReturnArrowToPool(GameObject arrow)
    {
        arrow.SetActive(false);
        _arrowPool.Enqueue(arrow);
    }
}
