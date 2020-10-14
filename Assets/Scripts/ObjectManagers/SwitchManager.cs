using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public ObjectType objectType = ObjectType.switch_;
    public int switchId = 1;

    private void Start()
    {
        GameManager.switches.Add(switchId, this);
    }
}
