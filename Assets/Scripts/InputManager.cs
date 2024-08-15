using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoSingleton<InputManager>
{
    public Vector2 startPosition;
    public Vector2 lastFramePosition;
    public Vector2 moveVector;
    public float moveSpeed;
    public Text text;
    public Vector2 touchPosition;
    public Touch currentMoveTouch;
    public float currentFingerId;
    public Vector2 currentDealtaPosition;
    public Ray myray;
    public RaycastHit2D hit;

    private void Start()
    {


        currentMoveTouch.fingerId = -1;

    }


    private void Update()
    {
        currentFingerId = currentMoveTouch.fingerId;
        currentDealtaPosition = currentMoveTouch.deltaPosition;
        print("几个手指" + Input.touchCount);
        if (Input.touchCount > 0)
        {
            
            foreach (var touch in Input.touches)
            {
                print($"touch的, fingerID: {touch.fingerId}   Position: {touch.position}   Phase: {touch.phase}  Dealta:{touch.deltaPosition} ");

                if (touch.phase == TouchPhase.Began)
                {
                    myray = Camera.main.ScreenPointToRay(touch.position);
                    hit = Physics2D.Raycast(myray.origin, myray.direction, 20f);
                    if (hit.collider != null || currentMoveTouch.fingerId != -1)
                    {
                        continue;
                    }
                    else
                    {
                        currentMoveTouch = touch;
                    }
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    if (currentMoveTouch.fingerId == touch.fingerId)
                    {
                        currentMoveTouch = touch;
                    }
                }
                if (touch.phase == TouchPhase.Stationary)
                {
                    if (currentMoveTouch.fingerId == touch.fingerId)
                    {
                        currentMoveTouch = touch;
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (currentMoveTouch.fingerId == touch.fingerId)
                    {
                        currentMoveTouch.fingerId = -1;
                    }
                }
            }
        }

        if (currentMoveTouch.fingerId != -1)
        {
            moveVector = currentMoveTouch.deltaPosition;
            transform.Translate(-moveVector * moveSpeed);
            if(transform.position.x < -62.67f || transform.position.x > -1.12f || transform.position.y < -27.7f || transform.position.y > -7.5f)
            {
                transform.position = new Vector2(Mathf.Clamp(transform.position.x, -62.67f, -1.12f), Mathf.Clamp(transform.position.y, -27.7f, -7.5f));
            }
           

        }
        print($" current的 fingerID: {currentMoveTouch.fingerId}   Position: {currentMoveTouch.position}   Phase: {currentMoveTouch.phase}  Dealta:{currentMoveTouch.deltaPosition} ");
    }


    //private void OnMouseDown()
    //{
    //    text.text = "OnMouseDown + Camera";
    //    lastFramePosition = Input.mousePosition;

    //}

    //private void OnMouseUp()
    //{


    //}

    //private void OnMouseDrag()
    //{
    //    text.text = "OnMouseDrag + Camera";
    //    moveVector = (lastFramePosition - (Vector2)Input.mousePosition).normalized * moveSpeed;
    //    transform.Translate(moveVector);

    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(myray.origin,myray.direction * 20f);

    }


}
