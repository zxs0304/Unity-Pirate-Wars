using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public CinemachineVirtualCamera mainCamera;
    public Transform character;

    public void SetCameraFollow()
    {
        mainCamera.Follow = character;

    }
}
