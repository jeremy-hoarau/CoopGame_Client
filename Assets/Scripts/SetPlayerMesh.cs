using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerMesh : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject meshRobot1, meshRobot2;

    private void Start()
    {
        if (playerManager.id == 1)
        {
            meshRobot1.SetActive(true);
        }
        else
        {
            meshRobot2.SetActive(true);
        }
    }
}
