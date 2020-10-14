using UnityEngine;

public class SetActiveInTrigger : MonoBehaviour
{
    public GameObject objectToActivate;
    public bool deactivateOnExit = true;
    
    private void OnTriggerEnter(Collider other)
    {
        objectToActivate.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(deactivateOnExit)
            objectToActivate.SetActive(false);
    }
}
