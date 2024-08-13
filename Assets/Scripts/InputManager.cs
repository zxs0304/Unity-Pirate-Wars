using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            foreach (var item in Input.touches)
            {
                print( $" 手指ID:{item.fingerId}  手指位置：{item.position} 手指按压：{item.pressure} 手指阶段：{item.phase} 手指类别:{item.type} 点击次数：{item.tapCount}");
            }
        }

    }


}
