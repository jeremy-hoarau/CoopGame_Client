using System;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu instance;

    public KeyCode inGameMenuKeyCode = KeyCode.Escape;
    public TextMeshProUGUI label;
    public TMP_Dropdown dropdown;
    public Canvas settingsCanvas;
    
    private Canvas canvas;
    private GraphicRaycaster graphicRaycaster;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(transform.parent.gameObject);
        }
    }
    
    private void Start()
    {
        canvas = GetComponent<Canvas>();
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        canvas.enabled = false;
        graphicRaycaster.enabled = false;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && !settingsCanvas.enabled && Input.GetKeyDown(inGameMenuKeyCode))
        {
            ToggleInGameMenu();
        }
    }

    public void Resume()
    {
        HideInGameMenu();
    }

    public void LoadLevel()
    {
        int _sceneIndex = Int32.Parse(label.text[label.text.Length - 1].ToString());
        ClientSend.LoadScene(_sceneIndex);
        Resume();
    }

    public void GoToSettings()
    {
        canvas.enabled = false;
        settingsCanvas.enabled = true;
        settingsCanvas.gameObject.GetComponent<UI_Settings>().SetSliderValues();
    }

    public void Disconnect()
    {
        Client.instance.Disconnect();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(player);
        }
        GameManager.players.Clear();
        canvas.enabled = false;
        SceneManager.LoadScene(0);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    
    
    private void ShowInGameMenu()
    {
        canvas.enabled = true;
        graphicRaycaster.enabled = true;
        
        Cursor.lockState = CursorLockMode.None;
        dropdown.value = SceneManager.GetActiveScene().buildIndex - 1;
        if (GameManager.players.ContainsKey(Client.instance.myId))
        {
            GameManager.players[Client.instance.myId].stopInputs = true;
            DisableFreeLookCamera();
        }
    }

    private void HideInGameMenu()
    {
        canvas.enabled = false;
        graphicRaycaster.enabled = false;
        
        Cursor.lockState = CursorLockMode.Locked;
        if (GameManager.players.ContainsKey(Client.instance.myId))
        {
            GameManager.players[Client.instance.myId].stopInputs = false;
            EnableFreeLookCamera();
        }
    }

    private void ToggleInGameMenu()
    {
        if(canvas.enabled)
            HideInGameMenu();
        else
            ShowInGameMenu();
    }

    private void EnableFreeLookCamera()
    {
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera != null && mainCamera.transform.childCount >= 1 && mainCamera.transform.GetChild(0).TryGetComponent(out CinemachineFreeLook cinemachineFreeLook))
        {
            mainCamera.GetComponentInChildren<LoadSensibility>().LoadSensibilitySettings();
        }
    }
    
    private void DisableFreeLookCamera()
    {
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera != null && mainCamera.transform.childCount >= 1 && mainCamera.transform.GetChild(0).TryGetComponent(out CinemachineFreeLook cinemachineFreeLook))
        {
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0;
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0;
        }
    }
}
