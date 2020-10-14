using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public InputField usernameField, IPAddressField;

    private Canvas canvas, settingsCanvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        settingsCanvas = GameObject.FindWithTag("UI_Settings").GetComponent<Canvas>();
        IPAddressField.text = Client.instance.ip;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(gameObject);
        }
    }

    public void SetIP()
    {
        Client.instance.ip = IPAddressField.text;
    }

    public void ConnectToServer()
    {
        Client.instance.ConnectToServer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void GoToSettings()
    {
        canvas.enabled = false;
        settingsCanvas.enabled = true;
        settingsCanvas.gameObject.GetComponent<UI_Settings>().SetSliderValues();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
