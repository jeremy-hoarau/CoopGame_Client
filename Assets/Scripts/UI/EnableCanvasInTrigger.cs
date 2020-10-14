using UnityEngine;

public class EnableCanvasInTrigger : MonoBehaviour
{
    public Canvas canvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            canvas.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            canvas.enabled = false;
    }
}
