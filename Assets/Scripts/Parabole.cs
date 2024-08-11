using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parabole : MonoBehaviour
{
    public Vector2 mousePosition;
    public LineRenderer line1; //����켣��
    public LineRenderer line2; //���������֮�������ֱ��
    public int line1Num = 8;
    private Vector3[] line1Points; //�����ߵĵ㼯
    public int line2Num = 2;
    private Vector3[] line2Points;
    public float maxForce = 8f;
    public float addForce;
    public float fixedForce = 3.5f;
    public float gravityForce = 5f;
    public float rotate = 5; //����ʱʩ�ӵ�Ť�ش�С
    
    private Rigidbody2D rb;
    public float pointsGap = 0.2f; //ʱ���ϸ�̶ֿȣ���ֵԽС��������Խ׼ȷ
    public Vector2 releaseVelocity; //�ͷ���һ��ʩ�ӵ���
    public GameObject dragPoint;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        line1.positionCount = line1Num;
        line2.positionCount = line2Num;
        line1Points = new Vector3[line1Num];
        line2Points = new Vector3[line2Num];

        line1.enabled = false;
        line2.enabled = false;
        dragPoint.SetActive(false);
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private void OnMouseDown()
    {
        line1.enabled = true;
        line2.enabled = true;
        dragPoint.SetActive(true);
    }

    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        addForce = Vector2.Distance(mousePosition, transform.position);
        addForce =  Mathf.Clamp(addForce, 0, maxForce);

        line2Points[0] = transform.position;
        line2Points[1] = line2Points[0] + ((Vector3)mousePosition - line2Points[0]).normalized * addForce;
        line2.SetPositions(line2Points);

        dragPoint.transform.position = line2Points[1];
        for (int t = 0; t < line1Num; t++)
        {
            line1Points[t] = (Vector2)transform.position
                + (((Vector2)transform.position - mousePosition).normalized * addForce * fixedForce / rb.mass ) *  (t * pointsGap)
                + (Physics2D.gravity * gravityForce) * 0.5f * (t * pointsGap) * (t * pointsGap);
            //��ƽ���˶����������߶�Ϊhʱ�������������ٶȣ�����ٿ����˶���ʱ�䣬�������Ҫ������ͬ��λ�ã���ô��Ҫ���ӳ�ʼ�����������ʹﵽ�ˡ�������ͬλ�ã��ӿ��˶����̡���Ч����
        }

        line1.SetPositions(line1Points);
        releaseVelocity = ((Vector2)transform.position - mousePosition).normalized * addForce * fixedForce;

    }

    private void OnMouseUp()
    {

        rb.AddForce(releaseVelocity,ForceMode2D.Impulse);
        rb.AddTorque(rotate, ForceMode2D.Impulse);

        releaseVelocity = Vector2.zero;
        addForce = 0;
        line1.enabled = false;
        line2.enabled = false;
        dragPoint.SetActive(false);
        GetComponent<Bomb>().throwing = true;

    }





}
