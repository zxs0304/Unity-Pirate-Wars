using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public CinemachineVirtualCamera mainCamera;
    public Transform character;
    public float moveSpeed = 0.1f;

    public void SetCameraFollow(Transform transform)
    {
        mainCamera.Follow = transform;

    }

    public void MoveCameraPosition(Vector2 vector2 )
    {
        mainCamera.transform.Translate(vector2.normalized * moveSpeed);

    }
    

}
