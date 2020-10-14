using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public ObjectType objectType = ObjectType.door;
    public int doorId = 1;

    private void Start()
    {
        GameManager.doors.Add(doorId, this);
    }
}
