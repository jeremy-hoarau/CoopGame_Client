using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOtherPlayerPosition : MonoBehaviour
{
    private int _id;
    
    private void Start()
    {
        _id = GetComponent<PlayerManager>().id;
    }

    private void FixedUpdate()
    {
        transform.position = GameManager.players[_id].position;
    }
}
