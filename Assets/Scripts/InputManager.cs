using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    public Vector2 touchStartPosition;
    public Vector2 moveVector;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if(touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                moveVector = touch.position - touchStartPosition;
                CameraManager.Instance.MoveCameraPosition(moveVector);
            }
            //foreach (var item in Input.touches)
            //{
            //    print( $" 手指ID:{item.fingerId}  手指位置：{item.position} 手指按压：{item.pressure} 手指阶段：{item.phase} 手指类别:{item.type} 点击次数：{item.tapCount}");
            //}
        }

    }


}
