using System;
using UnityEngine;

public class GrappleIndicator : MonoBehaviour
{
    public GameObject indicator;

    private PlayerManager playerManager;
    private bool isInGrappleRange;
    private Vector3 grapplePoint;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (playerManager.isGrappling || !isInGrappleRange)
        {
            indicator.SetActive(false);
        }
        else if(isInGrappleRange && !indicator.activeSelf)
        {
            indicator.SetActive(true);
        }

        if (indicator.activeSelf)
        {
            indicator.transform.LookAt(grapplePoint, Vector3.down);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrapplePoint"))
        {
            isInGrappleRange = true;
            grapplePoint = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GrapplePoint"))
        {
            isInGrappleRange = false;
        }
    }
}
