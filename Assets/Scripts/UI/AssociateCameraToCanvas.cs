using System.Collections;
using UnityEngine;

public class AssociateCameraToCanvas : MonoBehaviour
{
    private Canvas canvas;
    private GameObject mainCamera;
    
    void Start()
    {
        canvas = GetComponent<Canvas>();
        StartCoroutine(GetMainCamera());
    }

    IEnumerator GetMainCamera()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera == null)
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(GetMainCamera());
        }
        else
        {
            canvas.worldCamera = mainCamera.GetComponent<Camera>();
            canvas.planeDistance = 0.2f;
        }
    }
}
