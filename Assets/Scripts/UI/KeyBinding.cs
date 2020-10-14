using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBinding : MonoBehaviour
{
    public enum Control
    {
        Forward,
        Backward,
        Left,
        Right,
        Jump,
        Interact,
        Grapple,
        Grab
    }

    public Color changingKeyButtonColor;

    public Button forward_KeyCode,
        backward_KeyCode,
        left_KeyCode,
        right_KeyCode,
        jump_KeyCode,
        interact_KeyCode,
        grapple_KeyCode,
        grab_KeyCode;
    
    private TextMeshProUGUI forward_KeyCode_Text,
        backward_KeyCode_Text,
        left_KeyCode_Text,
        right_KeyCode_Text,
        jump_KeyCode_Text,
        interact_KeyCode_Text,
        grapple_KeyCode_Text,
        grab_KeyCode_Text;

    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));
    private Control control;
    private Dictionary<int, Control> IdToControl = new Dictionary<int, Control>();
    private Color defaultColor;
    private bool isListening;

    private void Start()
    {
        defaultColor = forward_KeyCode.image.color;
        IdToControl = new Dictionary<int, Control>()
        {
            {0, Control.Forward},
            {1, Control.Backward},
            {2, Control.Left},
            {3, Control.Right},
            {4, Control.Jump},
            {5, Control.Interact},
            {6, Control.Grapple},
            {7, Control.Grab},
        };
        forward_KeyCode_Text = forward_KeyCode.GetComponentInChildren<TextMeshProUGUI>();
        backward_KeyCode_Text = backward_KeyCode.GetComponentInChildren<TextMeshProUGUI>();
        left_KeyCode_Text = left_KeyCode.GetComponentInChildren<TextMeshProUGUI>();
        right_KeyCode_Text = right_KeyCode.GetComponentInChildren<TextMeshProUGUI>();
        jump_KeyCode_Text = jump_KeyCode.GetComponentInChildren<TextMeshProUGUI>();
        interact_KeyCode_Text = interact_KeyCode.GetComponentInChildren<TextMeshProUGUI>();
        grapple_KeyCode_Text = grapple_KeyCode.GetComponentInChildren<TextMeshProUGUI>();
        grab_KeyCode_Text = grab_KeyCode.GetComponentInChildren<TextMeshProUGUI>();
        GetBindingSettings();
    }

    private void LateUpdate()
    {
        if (isListening && Input.anyKeyDown)
        {
            foreach (KeyCode _keyCode in keyCodes)
            {
                if (Input.GetKeyDown(_keyCode)) {
                    BindKey(_keyCode);
                    StopListeningInput();
                    break;
                }
            }
        }
    }

    private void GetBindingSettings()
    {
        if (PlayerPrefs.HasKey("Key_Forward"))
            forward_KeyCode_Text.text = PlayerPrefs.GetString("Key_Forward");
        if (PlayerPrefs.HasKey("Key_Backward"))
            backward_KeyCode_Text.text = PlayerPrefs.GetString("Key_Backward");
        if (PlayerPrefs.HasKey("Key_Left"))
            left_KeyCode_Text.text = PlayerPrefs.GetString("Key_Left");
        if (PlayerPrefs.HasKey("Key_Right"))
            right_KeyCode_Text.text = PlayerPrefs.GetString("Key_Right");
        if (PlayerPrefs.HasKey("Key_Jump"))
            jump_KeyCode_Text.text = PlayerPrefs.GetString("Key_Jump");
        if (PlayerPrefs.HasKey("Key_Grapple"))
            grab_KeyCode_Text.text = PlayerPrefs.GetString("Key_Grapple");
        if (PlayerPrefs.HasKey("Key_Interact"))
            interact_KeyCode_Text.text = PlayerPrefs.GetString("Key_Interact");
        if (PlayerPrefs.HasKey("Key_Grab"))
            grab_KeyCode_Text.text = PlayerPrefs.GetString("Key_Grab");
    }

    private void BindKey(KeyCode _keyCode)
    {
        string _key = Convert.ToString(_keyCode);

        switch (control)
        {
            case Control.Forward:
                if(GameManager.players.ContainsKey(Client.instance.myId))
                    GameManager.players[Client.instance.myId].GetComponent<PlayerController>().forward_key = _keyCode;
                PlayerPrefs.SetString("Key_Forward", _key);
                forward_KeyCode_Text.text = _key;
                forward_KeyCode.image.color = defaultColor;
                break;
            case Control.Backward:
                if(GameManager.players.ContainsKey(Client.instance.myId))
                    GameManager.players[Client.instance.myId].GetComponent<PlayerController>().backward_key = _keyCode;
                PlayerPrefs.SetString("Key_Backward", _key);
                backward_KeyCode_Text.text = _key;
                backward_KeyCode.image.color = defaultColor;
                break;
            case Control.Left:
                if(GameManager.players.ContainsKey(Client.instance.myId))
                    GameManager.players[Client.instance.myId].GetComponent<PlayerController>().left_key = _keyCode;
                PlayerPrefs.SetString("Key_Left", _key);
                left_KeyCode_Text.text = _key;
                left_KeyCode.image.color = defaultColor;
                break;
            case Control.Right:
                if(GameManager.players.ContainsKey(Client.instance.myId))
                    GameManager.players[Client.instance.myId].GetComponent<PlayerController>().right_key = _keyCode;
                PlayerPrefs.SetString("Key_Right", _key);
                right_KeyCode_Text.text = _key;
                right_KeyCode.image.color = defaultColor;
                break;
            case Control.Jump:
                if(GameManager.players.ContainsKey(Client.instance.myId))
                    GameManager.players[Client.instance.myId].GetComponent<PlayerController>().jump_key = _keyCode;
                PlayerPrefs.SetString("Key_Jump", _key);
                jump_KeyCode_Text.text = _key;
                jump_KeyCode.image.color = defaultColor;
                break;
            case Control.Interact:
                if(GameManager.players.ContainsKey(Client.instance.myId))
                    GameManager.players[Client.instance.myId].GetComponent<PlayerController>().interact_key = _keyCode;
                PlayerPrefs.SetString("Key_Interact", _key);
                interact_KeyCode_Text.text = _key;
                interact_KeyCode.image.color = defaultColor;
                break;
            case Control.Grapple:
                if(GameManager.players.ContainsKey(Client.instance.myId))
                    GameManager.players[Client.instance.myId].GetComponent<PlayerController>().grapple_key = _keyCode;
                PlayerPrefs.SetString("Key_Grapple", _key);
                grapple_KeyCode_Text.text = _key;
                grapple_KeyCode.image.color = defaultColor;
                break;
            case Control.Grab:
                if(GameManager.players.ContainsKey(Client.instance.myId))
                    GameManager.players[Client.instance.myId].GetComponent<PlayerController>().grab_key = _keyCode;
                PlayerPrefs.SetString("Key_Grab", _key);
                grab_KeyCode_Text.text = _key;
                grab_KeyCode.image.color = defaultColor;
                break;
        };
    }

    public void StartListeningInput(int _controlId)
    {
        control = IdToControl[_controlId];
        isListening = true;
    }

    public void ChangeColorToSelected(Image _image)
    {
        _image.color = changingKeyButtonColor;
    }

    public void StopListeningInput()
    {
        isListening = false;
    }
}
