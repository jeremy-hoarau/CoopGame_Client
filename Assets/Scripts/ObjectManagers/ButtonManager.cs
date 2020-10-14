using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public ObjectType objectType = ObjectType.button;
    public int buttonId = 1;
    
    private void Start()
    {
        GameManager.buttons.Add(buttonId, this);
    }
}
