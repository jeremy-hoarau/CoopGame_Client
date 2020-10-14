using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode forward_key = KeyCode.Z,
        backward_key = KeyCode.S,
        left_key = KeyCode.Q,
        right_key = KeyCode.D,
        jump_key = KeyCode.Space,
        grapple_key = KeyCode.F,
        interact_key = KeyCode.E,
        grab_key = KeyCode.Mouse0;
    public GameObject Camera;
    public Animator animatorRobot_1, animatorRobot_2;

    private Animator animator;
    private PlayerManager playerManager;
    private bool[] moveInputs;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerManager = GetComponent<PlayerManager>();
        animator = playerManager.id == 1 ? animatorRobot_1 : animatorRobot_2;
        GetKeyBindings();
    }

    private void FixedUpdate()
    {
        transform.position = GameManager.players[Client.instance.myId].position;
        
        SendInputToServer();
        UpdateAnimation();
    }

    private void GetKeyBindings()
    {
        if (PlayerPrefs.HasKey("Key_Forward"))
            forward_key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Forward"));
        if (PlayerPrefs.HasKey("Key_Backward"))
            backward_key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Backward"));
        if (PlayerPrefs.HasKey("Key_Left"))
            left_key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Left"));
        if (PlayerPrefs.HasKey("Key_Right"))
            right_key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Right"));
        if (PlayerPrefs.HasKey("Key_Jump"))
            jump_key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Jump"));
        if (PlayerPrefs.HasKey("Key_Grapple"))
            grapple_key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Grapple"));
        if (PlayerPrefs.HasKey("Key_Interact"))
            interact_key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Interact"));
        if (PlayerPrefs.HasKey("Key_Grab"))
            grab_key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Grab"));
    }
    
    private void SendInputToServer()
    {
        bool[] _inputs;
        
        if (GameManager.players[Client.instance.myId].stopInputs)
        {
            moveInputs = new []
            {
                false,
                false,
                false,
                false
            };

            _inputs = new[]
            {
                false,
                false,
                false,
                false
            };
            
            ClientSend.PlayerMovement(moveInputs, Camera.transform.position);
            ClientSend.PlayerInputs(_inputs);
            
            return;
        }

        moveInputs = new []
        {
            Input.GetKey(right_key),
            Input.GetKey(left_key),
            Input.GetKey(forward_key),
            Input.GetKey(backward_key)
        };
        // float[] _moveInputs =
        // {
        //     Input.GetAxis("Horizontal"),
        //     Input.GetAxis("Vertical"),
        // };
        
        //align the player with the camera only when he tries to move or if he is grabbing an object
        if (GameManager.players[Client.instance.myId].isGrabbing || Input.GetKey(right_key) || Input.GetKey(left_key) || Input.GetKey(forward_key) || Input.GetKey(backward_key))
        {
            Vector3 pos = transform.position;
            Vector3 _lookAtTarget = pos + pos - Camera.transform.position;
            _lookAtTarget.y = pos.y;
            
            transform.LookAt(_lookAtTarget);
        }

        ClientSend.PlayerMovement(moveInputs, Camera.transform.position);


        _inputs = new[]
        {
            Input.GetKey(jump_key),
            Input.GetKey(grapple_key),
            Input.GetKey(interact_key),
            Input.GetKey(grab_key)
        };

        ClientSend.PlayerInputs(_inputs);
    }

    private void UpdateAnimation()
    {
        if(moveInputs[0])
            animator.SetFloat("Horizontal", 1);
        else if(moveInputs[1])
            animator.SetFloat("Horizontal", -1);
        else
            animator.SetFloat("Horizontal", 0);
        
        if(moveInputs[2])
            animator.SetFloat("Vertical", 1);
        else if(moveInputs[3])
            animator.SetFloat("Vertical", -1);
        else
            animator.SetFloat("Vertical", 0);
    }
    
}
