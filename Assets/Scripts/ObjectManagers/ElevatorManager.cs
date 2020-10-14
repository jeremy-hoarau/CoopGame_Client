using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public ObjectType objectType = ObjectType.elevator;
    public int elevatorId = 1;

    [HideInInspector] public Vector3 position;
    [HideInInspector] public Quaternion rotation;

    void Start()
    {
        position = transform.position;
        rotation = transform.rotation;
        GameManager.elevators.Add(elevatorId, this);
    }

    private void FixedUpdate()
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
