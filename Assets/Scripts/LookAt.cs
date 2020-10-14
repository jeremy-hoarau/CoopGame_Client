using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    
    void FixedUpdate()
    {
        transform.LookAt(target.transform.position + offset);
    }
}
