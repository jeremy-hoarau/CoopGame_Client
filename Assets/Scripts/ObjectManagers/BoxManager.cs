using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public ObjectType objectType = ObjectType.box;
    public int boxId = 1;
    
    [HideInInspector] public Vector3 position;
    [HideInInspector] public Quaternion rotation;

    private void Start()
    {
        position = transform.position;
        rotation = transform.rotation;
        GameManager.boxes.Add(boxId, this);
    }

    private void FixedUpdate()
    {
        Transform tr = transform;
        if(position != tr.position)
            tr.position = position;
        if(rotation  != tr.rotation)
            tr.rotation = rotation;
    }
}
