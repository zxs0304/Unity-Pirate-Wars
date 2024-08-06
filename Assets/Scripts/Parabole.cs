using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum STATE
{
    None = -1,
    Idle = 0,
    Grab, //抓取
    Drag, //拖拽
    Release,
    Land,
    Num
}


public class Parabole : MonoBehaviour
{
    private Vector2 mousePosition;
    public LineRenderer parabolaLine;
    public int line1Num = 10;
    private Vector3[] points1;
    public LineRenderer forceLine;
    public float maxForce = 2f;
    public int line2Num = 2;
    private Vector3[] points2;

    Rigidbody2D rb;
    public float releaseForce;
    public float pointsGap = 0.2f; 
    Vector2 releaseVelocity;
    float Sx;
    float t;
    float g = 9.8f;
    float height;
    float Xgap = 0.1f;

    public GameObject dragPoint;
    
    private STATE state = STATE.Idle; //初始状态
    private STATE nextState = STATE.None;//下一个状态

    private void Start()
    {
        parabolaLine.positionCount = line1Num;
        forceLine.positionCount = line2Num;

        points1 = new Vector3[line1Num];
        points2 = new Vector3[line2Num];
        rb = GetComponent<Rigidbody2D>();

        print(transform.right);
        print(Physics.gravity);
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        parabolaLine.positionCount = line1Num;
        


        //switch (state)
        //{
        //    case STATE.Release:

        //        break;

        //}

        //if(nextState != STATE.None)
        //{
        //    state = nextState;
        //    nextState = STATE.None;

        //    switch (state)
        //    {
        //        case STATE.Grab:
        //            rb.gravityScale = 0;
        //            parabolaLine.enabled = true;
        //            dragPoint.SetActive(true);
        //            nextState = STATE.Drag;

        //            break;

        //        case STATE.Release:
        //            rb.drag = 0;
        //            rb.gravityScale = 1;
        //            rb.velocity = releaseVelocity;
        //            forceLine.enabled = false;
        //            dragPoint.SetActive(false);
        //            break;


        //    }

        //}

        //switch (state)
        //{
        //    case STATE.Drag:
        //        points2[0] = transform.position;
        //        points2[1] = mousePosition;

        //        if (Vector3.Distance(points2[0] , points2[1]) > maxForce)
        //        {
        //            points2[1] = points2[0] + (points2[1] - points2[0]).normalized * maxForce;
        //        }
        //        forceLine.SetPositions(points2);
        //        dragPoint.transform.position = points2[1];

        //        releaseVelocity = (points2[0] - points2[1]) * releaseForce;
        //        //落地的最大水平位移
        //        Sx = releaseVelocity.x * (releaseVelocity.y + Mathf.Sqrt((releaseVelocity.y * releaseVelocity.y / g / g) + 2 * height / g));
        //        //根据水平位移确定轨迹的X轴绘制间隔
        //        //结合LineRender绘制图像for(int i=0;i<line1Num;i+)
        //        //points1[il.x = transform.position.x + i * xUnit; points1[il.y = GetFuncPathY(points1[i].x);
        //        //line1.SetPositions(points1);



        //        break;
        //    case STATE.Release:
        //        parabolaLine.enabled = false;
        //        break;


        //}

    }

    private void OnMouseDown()
    {

    }

    private void OnMouseDrag()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print(mousePosition);
        print(transform.position);
        print(Physics.gravity);
        for (int t = 0; t < line1Num; t++)
        {
            points1[t] = (Vector2)transform.position
                + ((Vector2)transform.position - mousePosition).normalized * releaseForce * t * pointsGap
                + (Vector2)Physics.gravity * 0.5f * t * pointsGap * t * pointsGap;
        }

        parabolaLine.SetPositions(points1);


    }

    private void OnMouseUp()
    {
        nextState = STATE.Release;
    }





}
