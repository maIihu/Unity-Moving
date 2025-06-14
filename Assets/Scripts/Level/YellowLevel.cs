using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLevel : MonoBehaviour
{
    [SerializeField] private GameObject[] yellowGround;
    [SerializeField] private float timeToHandle;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private GameObject bot;

    private void Start()
    {
        GameObject playerSpawn = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        if (Camera.main != null) Camera.main.GetComponent<CameraFollow>().target = playerSpawn.transform;
        InvokeRepeating(nameof(HandleYellowGround), timeToHandle, timeToHandle * 2);
    }

    private void HandleYellowGround()
    {
        if (bot) bot.SetActive(!bot.activeSelf);

        foreach (var item in yellowGround)
        {
            ToggleObject(item);
        }
    }

    private void ToggleObject(GameObject obj)
    {
        if (obj == null) return;

        var col = obj.GetComponent<Collider2D>();
        var render = obj.GetComponent<SpriteRenderer>();

        if (col != null) col.enabled = !col.enabled;
        if (render != null) render.enabled = !render.enabled;
    }

}
