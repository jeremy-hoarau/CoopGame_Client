using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tuto_Move : MonoBehaviour
{
    private void Awake()
    {
        TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();
        if (Application.systemLanguage == SystemLanguage.French)
        {
            textMesh.text = textMesh.text.Replace("WASD", "ZQSD");
        }
    }
}
