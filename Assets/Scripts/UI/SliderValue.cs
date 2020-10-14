using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SliderValue : MonoBehaviour
{
    public Slider slider;
    public string numberPrecision = "0.0";

    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = slider.value.ToString(numberPrecision);
    }

    public void UpdateValue()
    {
        textMeshPro.text = slider.value.ToString(numberPrecision);
    }
}
