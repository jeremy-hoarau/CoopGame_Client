using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ButtonManager> buttons = new Dictionary<int, ButtonManager>();
    public static Dictionary<int, DoorManager> doors = new Dictionary<int, DoorManager>();
    public static Dictionary<int, SwitchManager> switches = new Dictionary<int, SwitchManager>();
    public static Dictionary<int, ElevatorManager> elevators = new Dictionary<int, ElevatorManager>();
    public static Dictionary<int, PlatformManager> platforms = new Dictionary<int, PlatformManager>();
    public static Dictionary<int, SwapPlatformManager> swapPlatforms = new Dictionary<int, SwapPlatformManager>();
    public static Dictionary<int, BoxManager> boxes = new Dictionary<int, BoxManager>();
    
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject waitingScreen;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(gameObject);
        }
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetObjects;
    }

    private void ResetObjects(Scene _scene, LoadSceneMode _mode)
    {
        buttons = new Dictionary<int, ButtonManager>();
        doors = new Dictionary<int, DoorManager>();
        switches = new Dictionary<int, SwitchManager>();
        elevators = new Dictionary<int, ElevatorManager>();
        platforms = new Dictionary<int, PlatformManager>();
        swapPlatforms = new Dictionary<int, SwapPlatformManager>();
        boxes = new Dictionary<int, BoxManager>();
    }

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        _player.GetComponent<PlayerManager>().position = _position;
        _player.GetComponent<PlayerManager>().inputs = new bool[4];
        _player.GetComponent<PlayerManager>().isGrappling = false;
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public static void UpdateObjectPosition(ObjectType _objectType, int _objectId, Vector3 _objectPosition)
    {
        switch (_objectType)
        {
            case (ObjectType.button):
                buttons[_objectId].transform.position = _objectPosition;
                break;
            case (ObjectType.door):
                doors[_objectId].transform.position = _objectPosition;
                break;
            case (ObjectType.switch_):
                switches[_objectId].transform.position = _objectPosition;
                break;
            case (ObjectType.elevator):
                elevators[_objectId].position = _objectPosition; // *
                break;
            case (ObjectType.platform):
                platforms[_objectId].position = _objectPosition; // *
                break;
            case (ObjectType.box):
                boxes[_objectId].position = _objectPosition; // *
                break;
        }
    }
    // * = Update the field position and not the transform to use it later in the FixedUpdate to avoid jitters for objects moving with the player
    
    public static void UpdateObjectRotation(ObjectType _objectType, int _objectId, Quaternion _objectRotation)
    {
        switch (_objectType)
        {
            case (ObjectType.button):
                buttons[_objectId].transform.rotation = _objectRotation;
                break;
            case (ObjectType.door):
                doors[_objectId].transform.rotation = _objectRotation;
                break;
            case (ObjectType.switch_):
                switches[_objectId].transform.rotation = _objectRotation;
                break;
            case (ObjectType.elevator):
                elevators[_objectId].rotation = _objectRotation;  // **
                break;
            case (ObjectType.platform):
                platforms[_objectId].rotation = _objectRotation;  // **
                break;
            case (ObjectType.box):
                boxes[_objectId].rotation = _objectRotation; // **
                break;
        }
    }
    // ** = Update the field rotation and not the transform to use it later in the FixedUpdate to avoid jitters for objects moving with the player

    public static void DestroyObject(ObjectType _objectType, int _objectId)
    {
        GameObject go;
        
        switch (_objectType)
        {
            case (ObjectType.button):
                go = buttons[_objectId].gameObject;
                buttons.Remove(_objectId);
                break;
            case (ObjectType.door):
                go = doors[_objectId].gameObject;
                doors.Remove(_objectId);
                break;
            case (ObjectType.switch_):
                go = switches[_objectId].gameObject;
                switches.Remove(_objectId);
                break;
            case (ObjectType.elevator):
                go = elevators[_objectId].gameObject;
                elevators.Remove(_objectId);
                break;
            case (ObjectType.platform):
                go = platforms[_objectId].gameObject;
                platforms.Remove(_objectId);
                break;
            case (ObjectType.box):
                go = boxes[_objectId].gameObject;
                boxes.Remove(_objectId);
                break;
            default:
                go = new GameObject();
                break;
        }
        
        Destroy(go);
    }
}
