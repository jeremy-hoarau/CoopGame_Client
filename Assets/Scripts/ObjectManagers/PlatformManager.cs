using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public ObjectType objectType = ObjectType.platform;
    public int platformId = 1;

    [HideInInspector] public Vector3 position;
    [HideInInspector] public Quaternion rotation;

    void Start()
    {
        position = transform.position;
        rotation = transform.rotation;
        GameManager.platforms.Add(platformId, this);
    }

    private void FixedUpdate()
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
