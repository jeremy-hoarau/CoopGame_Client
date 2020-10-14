using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    public Canvas inGameMenuCanvas;
    public Slider horizontalSensibilitySlider, verticalSensibilitySlider;
    public GameObject localPlayerPrefab;

    private CinemachineFreeLook playerCinemachineFreeLook;
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        playerCinemachineFreeLook = localPlayerPrefab.transform.GetChild(0).GetComponentInChildren<CinemachineFreeLook>();

        SetSliderValues();
    }

    private void LateUpdate()
    {
        if (canvas.enabled && Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackToMenu();
        }
    }

    public void SetSliderValues()
    {
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera != null && mainCamera.TryGetComponent(out CinemachineFreeLook cinemachineFreeLook))
        {
            horizontalSensibilitySlider.value =
                PlayerPrefs.GetFloat("HorizontalSensibility", cinemachineFreeLook.m_XAxis.m_MaxSpeed);
            verticalSensibilitySlider.value =
                PlayerPrefs.GetFloat("VerticalSensibility", cinemachineFreeLook.m_YAxis.m_MaxSpeed);
        }
        else
        {
            horizontalSensibilitySlider.value =
                PlayerPrefs.GetFloat("HorizontalSensibility", playerCinemachineFreeLook.m_XAxis.m_MaxSpeed);
            verticalSensibilitySlider.value =
                PlayerPrefs.GetFloat("VerticalSensibility", playerCinemachineFreeLook.m_YAxis.m_MaxSpeed);
        }
    }

    public void GoBackToMenu()
    {
        SaveSettings();
        canvas.enabled = false;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameObject.FindWithTag("UI_MainMenu").GetComponent<Canvas>().enabled = true;
        }
        else
        {
            inGameMenuCanvas.enabled = true;
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("HorizontalSensibility", horizontalSensibilitySlider.value);
        PlayerPrefs.SetFloat("VerticalSensibility", verticalSensibilitySlider.value);
        PlayerPrefs.Save();
    }
}
