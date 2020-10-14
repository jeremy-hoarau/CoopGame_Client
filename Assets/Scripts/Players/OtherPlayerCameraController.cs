using System;
using UnityEngine;

public class OtherPlayerCameraController : MonoBehaviour
{
    public GameObject cam;
    
    private int _id;
    
    private void Start()
    {
        _id = GetComponent<PlayerManager>().id;
    }

    private void FixedUpdate()
    {
        cam.transform.position = GameManager.players[_id].cameraPosition;
    }
}
