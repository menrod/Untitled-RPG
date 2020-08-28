using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] FloatingJoystick CamMovement;
    [SerializeField] Cinemachine.CinemachineFreeLook cmfreelook;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cmfreelook.m_XAxis.m_InputAxisValue = CamMovement.Horizontal;
        cmfreelook.m_YAxis.m_InputAxisValue = CamMovement.Vertical;
    }
}
