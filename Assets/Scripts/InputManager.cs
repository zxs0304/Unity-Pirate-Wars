using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoSingleton<InputManager>
{

    public float moveScreenSpeed;
    public Touch moveScreenTouch;
    public Touch movePlayerTouch;
    public RaycastHit2D hit;
    public Parabole currentParabole = null;

    //TEST
    public Vector2 currentDealtaPosition;
    public Ray myray;
    public Text text;



    private void Start()
    {


        moveScreenTouch.fingerId = -1;
        movePlayerTouch.fingerId = -1;
    }


    private void Update()
    {

        print("几个手指" + Input.touchCount);
        if (Input.touchCount > 0)
        {
            
            foreach (var touch in Input.touches)
            {
                //print($"touch的, fingerID: {touch.fingerId}   Position: {touch.position}   Phase: {touch.phase}  Dealta:{touch.deltaPosition} ");

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        RegisterTouch(touch);
                        break;
                    case TouchPhase.Moved:
                        UpdateTouch(touch);
                        break;
                    case TouchPhase.Stationary:
                        UpdateTouch(touch);
                        break;
                    case TouchPhase.Ended:
                        DeleteTouch(touch);
                        break;

                }
            }
        }


        //print($" current的 fingerID: {moveScreenTouch.fingerId}   Position: {moveScreenTouch.position}   Phase: {moveScreenTouch.phase}  Dealta:{moveScreenTouch.deltaPosition} ");
    }

    public void RegisterTouch(Touch touch) //根据点击那刻点到的物体 来判断此次点击是要拖动屏幕还是拖动玩家
    {
        myray = Camera.main.ScreenPointToRay(touch.position);
        hit = Physics2D.Raycast(myray.origin, myray.direction, 20f);
        print(hit.collider);
        if (hit.collider == null && moveScreenTouch.fingerId == -1)
        {
            print("什么都没点到");
            moveScreenTouch = touch;
        }
        else if (hit.collider != null && (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Bomb")) && movePlayerTouch.fingerId == -1)
        {
            print("点到了 " + hit.collider.name);
            movePlayerTouch = touch;
            currentParabole = hit.collider.GetComponent<Parabole>();
            currentParabole.OnTouchBegan();
        }
    }

    public void UpdateTouch(Touch touch)
    {
        if (touch.fingerId == moveScreenTouch.fingerId)
        {
            //移动屏幕
            //moveScreenTouch = touch;
            transform.Translate(-touch.deltaPosition * moveScreenSpeed);
            if (transform.position.x < -62.67f || transform.position.x > -1.12f || transform.position.y < -27.7f || transform.position.y > -7.5f)
            {
                transform.position = new Vector2(Mathf.Clamp(transform.position.x, -62.67f, -1.12f), Mathf.Clamp(transform.position.y, -27.7f, -7.5f));
            }
        }
        else if (touch.fingerId == movePlayerTouch.fingerId)
        {
            //movePlayerTouch = touch;
            Vector2 mousePosition =  Camera.main.ScreenToWorldPoint(touch.position);

            currentParabole.OnTouchDrag(mousePosition);
        }
    }

    public void DeleteTouch(Touch touch)
    {
        if (touch.fingerId == moveScreenTouch.fingerId)
        {
            moveScreenTouch.fingerId = -1;
        }
        else if (touch.fingerId == movePlayerTouch.fingerId)
        {
            movePlayerTouch.fingerId = -1;
            currentParabole.OnTouchEnded();
            currentParabole = null;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(myray.origin,myray.direction * 20f);

    }


}
