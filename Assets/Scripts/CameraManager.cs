using Cinemachine;
using LockstepTutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public CinemachineVirtualCamera movedCamera;
    public CinemachineVirtualCamera fixedCamera;
    public float moveSpeed = 0.1f;
    public Transform tempTransform;
    public void FixedCameraFollow(Transform transform)
    {
        fixedCamera.Follow = transform;

    }

    public void ActivateFixedCamera()
    {
        fixedCamera.Priority = 12;
        tempTransform = movedCamera.Follow;
        movedCamera.Follow = fixedCamera.Follow; //让movedCamera一直跟着，防止切回MovedCamera时候的瞬间移动
    }

    public void ActivateMovedCamera(Transform transform)
    {
        this.transform.position = transform.position;
        movedCamera.Follow = this.transform;
        fixedCamera.Priority = 10;
    }


    public void MoveCameraPosition(Vector2 vector2 )
    {


    }
    

}
