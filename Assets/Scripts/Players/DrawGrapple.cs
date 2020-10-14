using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGrapple : MonoBehaviour
{
    public Transform grapple_origin;
    
    private PlayerManager playerManager;
    private LineRenderer lineRenderer;

    private bool isDrawing = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerManager = GetComponent<PlayerManager>();
    }

    void Update()
    {
        if (GameManager.players[playerManager.id].isGrappling)
        {
            lineRenderer.SetPositions(new Vector3[] {grapple_origin.position, GameManager.players[playerManager.id].grapplingPoint});
            if (!isDrawing)
            {
                isDrawing = true;
                lineRenderer.enabled = true;
            }
        }
        else
        {
            isDrawing = false;
            lineRenderer.enabled = false;
        }
    }
}
