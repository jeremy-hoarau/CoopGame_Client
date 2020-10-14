using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }
    
    #region Packets

    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int) ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);
            
            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs, Vector3 _cameraPosition)
    {
        using (Packet _packet = new Packet((int) ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);
            _packet.Write(_cameraPosition);
            
            SendUDPData(_packet);
        }
    }
    
    public static void PlayerInputs(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int) ClientPackets.playerInputs))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            
            SendUDPData(_packet);
        }
    }

    public static void LoadScene(int _sceneIndex)
    {
        using (Packet _packet = new Packet((int) ClientPackets.loadScene))
        {
            _packet.Write(_sceneIndex);
            
            SendTCPData(_packet);
        }
    }
    #endregion
}
