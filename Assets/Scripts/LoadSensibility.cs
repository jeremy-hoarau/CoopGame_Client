using Cinemachine;
using UnityEngine;

public class LoadSensibility : MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook;
    
    private void Start()
    {
        LoadSensibilitySettings();
    }

    public void LoadSensibilitySettings()
    {
        if (PlayerPrefs.HasKey("HorizontalSensibility"))
        {
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = PlayerPrefs.GetFloat("HorizontalSensibility");
        }
        if (PlayerPrefs.HasKey("VerticalSensibility"))
        {
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = PlayerPrefs.GetFloat("VerticalSensibility");
        }
    }
}
