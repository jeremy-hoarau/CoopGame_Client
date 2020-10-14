using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public Vector3 position;
    public Vector3 cameraPosition;
    public bool[] inputs;
    public bool isGrappling, isGrabbing, stopInputs;
    public Vector3 grapplingPoint;
}
