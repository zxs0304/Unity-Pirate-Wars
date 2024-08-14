using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoSingleton<InputManager>
{
    public Vector2 touchStartPosition;
    public Vector2 moveVector;
    public Text text;

    private void Update()
    {
        //if(Input.touchCount > 0)
        //{
        //    Touch touch = Input.touches[0];
        //    if(touch.phase == TouchPhase.Began)
        //    {
        //        touchStartPosition = touch.position;
        //    }
        //    if (touch.phase == TouchPhase.Moved)
        //    {
        //        moveVector = touch.position - touchStartPosition;
        //        CameraManager.Instance.MoveCameraPosition(moveVector);
        //    }
            //foreach (var item in Input.touches)
            //{
            //    print( $" ��ָID:{item.fingerId}  ��ָλ�ã�{item.position} ��ָ��ѹ��{item.pressure} ��ָ�׶Σ�{item.phase} ��ָ���:{item.type} ���������{item.tapCount}");
            //}
        //}

    }

    private void OnMouseDown()
    {
        text.text = "OnMouseDown + Camera";

    }

    private void OnMouseUp()
    {
        text.text = "OnMouseUp + Camera";
    }

    private void OnMouseDrag()
    {
        text.text = "OnMouseDrag + Camera";
    }

}
