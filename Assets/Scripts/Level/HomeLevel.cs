using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeLevel : BaseLevel
{
    private void Start()
    {
        Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
    }
}
