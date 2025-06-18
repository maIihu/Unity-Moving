using System.Collections;
using UnityEngine;

public class BaseLevel : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected Transform playerSpawnPoint;

    protected void SpawnPlayer()
    {
        var playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
    }
    
    protected IEnumerator HandleGroundLoop(GameObject[] groundBlocks, float activeTime, float inactiveTime)
    {
        while (true)
        {
            foreach (var block in groundBlocks)
                SetObjectVisible(block, true);

            yield return new WaitForSeconds(activeTime);

            foreach (var block in groundBlocks)
                SetObjectVisible(block, false);

            yield return new WaitForSeconds(inactiveTime);
        }
    }
    
    private void SetObjectVisible(GameObject obj, bool isVisible)
    {
        if (!obj) return;

        if (obj.TryGetComponent(out Collider2D col)) col.enabled = isVisible;
        if (obj.TryGetComponent(out SpriteRenderer render)) render.enabled = isVisible;
    }

}
