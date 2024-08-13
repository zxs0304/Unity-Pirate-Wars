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
                print( $" ��ָID:{item.fingerId}  ��ָλ�ã�{item.position} ��ָ��ѹ��{item.pressure} ��ָ�׶Σ�{item.phase} ��ָ���:{item.type} ���������{item.tapCount}");
            }
        }

    }


}
