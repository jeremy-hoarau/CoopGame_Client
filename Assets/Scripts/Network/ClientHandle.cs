using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ObjectType
{
    door,
    button,
    switch_,
    elevator,
    platform,
    box
}

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();
        
        Debug.Log($"Message from Server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();
        
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        
        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPositionCameraPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Vector3 _cameraPosition = _packet.ReadVector3();

        GameManager.players[_id].position = _position;
        GameManager.players[_id].cameraPosition = _cameraPosition;
    }
    
    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void PlayerStartGrappling(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _grapplingPoint = _packet.ReadVector3();

        GameManager.players[_id].isGrappling = true;
        GameManager.players[_id].grapplingPoint = _grapplingPoint;
    }

    public static void PlayerStopGrappling(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.players[_id].isGrappling = false;
    }

    public static void PlayerGrabbingState(Packet _packet)
    {
        int _id = _packet.ReadInt();
        bool _isGrabbing = _packet.ReadBool();

        GameManager.players[_id].isGrabbing = _isGrabbing;
    }

    public static void OtherPlayerInputs(Packet _packet)
    {
        int _otherPlayerId = _packet.ReadInt();
        bool[] _otherPlayerInputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _otherPlayerInputs.Length; i++)
        {
            _otherPlayerInputs[i] = _packet.ReadBool();
        }
        
        if(GameManager.players.ContainsKey(_otherPlayerId))
            GameManager.players[_otherPlayerId].inputs = _otherPlayerInputs;
    }

    public static void RotatePlayer(Packet _packet)
    {
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[Client.instance.myId].gameObject.transform.rotation = _rotation;
    }
    
    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();
        
        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void LoadScene(Packet _packet)
    {
        int _sceneId = _packet.ReadInt();
        SceneManager.LoadScene(_sceneId);
    }

    public static void WaitingPlayers(Packet _packet)
    {
        GameManager.instance.waitingScreen.SetActive(true);
        if (GameManager.players.ContainsKey(Client.instance.myId))
        {
            GameManager.players[Client.instance.myId].stopInputs = true;
        }
    }

    public static void StopWaitingPlayers(Packet _packet)
    {
        GameManager.instance.waitingScreen.SetActive(false);
        if (GameManager.players.ContainsKey(Client.instance.myId))
        {
            GameManager.players[Client.instance.myId].stopInputs = false;
        }
    }

    public static void ObjectPosition(Packet _packet)
    {
        ObjectType objectType = objects[_packet.ReadInt()];
        int _objectId = _packet.ReadInt();
        Vector3 _objectPosition = _packet.ReadVector3();

        GameManager.UpdateObjectPosition(objectType, _objectId, _objectPosition);
    }
    
    public static void ObjectRotation(Packet _packet)
    {
        ObjectType objectType = objects[_packet.ReadInt()];
        int _objectId = _packet.ReadInt();
        Quaternion _objectRotation = _packet.ReadQuaternion();

        GameManager.UpdateObjectRotation(objectType, _objectId, _objectRotation);
    }

    public static void DestroyObject(Packet _packet)
    {
        ObjectType _objectType = objects[_packet.ReadInt()];
        int _objectId = _packet.ReadInt();
        
        GameManager.DestroyObject(_objectType, _objectId);
    }

    public static void SwapPlatformState(Packet _packet)
    {
        int _id = _packet.ReadInt();
        bool _activated = _packet.ReadBool();

        if (_activated)
            GameManager.swapPlatforms[_id].Activate();
        else
            GameManager.swapPlatforms[_id].Deactivate();
    }
    
    private static readonly Dictionary<int, ObjectType> objects = new Dictionary<int, ObjectType>()
    {
        {1, ObjectType.button},
        {2, ObjectType.door},
        {3, ObjectType.switch_},
        {4, ObjectType.elevator},
        {5, ObjectType.platform},
        {6, ObjectType.box},
    };

}