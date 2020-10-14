using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ResetCameraRotation : MonoBehaviour
{
    public CinemachineFreeLook CM_FreeLook;

    public void ResetRotation()
    {
        CM_FreeLook.m_XAxis.Value = 0f;
        CM_FreeLook.m_YAxis.Value = 0.3f;
    }
}
