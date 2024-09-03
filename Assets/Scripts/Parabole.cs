
using Lockstep.Logic;
using LockstepTutorial;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;


public class Parabole : MonoBehaviour
{
    public Vector2 mousePosition;
    public LineRenderer line1; //����켣��
    public LineRenderer line2; //���������֮�������ֱ��
    private Vector3[] line1Points; //�����ߵĵ㼯
    public int line2Num = 2;
    private Vector3[] line2Points;
    public float addForce;
    private Rigidbody2D rb;
    public Vector2 releaseVelocity; //�ͷ���һ��ʩ�ӵ���
    public GameObject dragPoint;
    public ParaboleData paraboleData;


    //TEST
    public Text text;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        text = GameObject.Find("Canvas").GetComponentInChildren<Text>(); ;
    }

    private void Start()
    {
        line1.positionCount = paraboleData.line1Num;
        line2.positionCount = 2;
        line1Points = new Vector3[paraboleData.line1Num];
        line2Points = new Vector3[2];

        line1.enabled = false;
        line2.enabled = false;
        dragPoint.SetActive(false);

    }

    public void OnTouchBegan()
    {
        text.text = "OnMouseDown + " + gameObject.name;
        line1.enabled = true;
        line2.enabled = true;
        dragPoint.SetActive(true);


    }

    public void OnTouchDrag(Vector2 mousePosition)
    {
        text.text = "OnMouseDrag + " + gameObject.name;

        addForce = Vector2.Distance(mousePosition, transform.position);
        addForce = Mathf.Clamp(addForce, 0, paraboleData.maxForce);
        line2Points[0] = transform.position;
        line2Points[1] = line2Points[0] + ((Vector3)mousePosition - line2Points[0]).normalized * addForce;
        line2.SetPositions(line2Points);
        dragPoint.transform.position = line2Points[1];
        for (int t = 0; t < paraboleData.line1Num; t++)
        {
            line1Points[t] = (Vector2)transform.position
                + (((Vector2)transform.position - mousePosition).normalized * addForce * paraboleData.fixedForce / rb.mass) * (t * paraboleData.pointsGap)
                + (Physics2D.gravity * paraboleData.gravityForce) * 0.5f * (t * paraboleData.pointsGap) * (t * paraboleData.pointsGap);
            //��ƽ���˶����������߶�Ϊhʱ�������������ٶȣ�����ٿ����˶���ʱ�䣬�������Ҫ������ͬ��λ�ã���ô��Ҫ���ӳ�ʼ�����������ʹﵽ�ˡ�������ͬλ�ã��ӿ��˶����̡���Ч����
        }
        line1.SetPositions(line1Points);
        releaseVelocity = ((Vector2)transform.position - mousePosition).normalized * addForce * paraboleData.fixedForce;

    }

    public void OnTouchEnded()
    {
        text.text = "OnMouseUp + " + gameObject.name;

        //GameManager.CurGameInput = new PlayerInput()
        //{
        //    forceX = releaseVelocity.x,
        //    forceY = releaseVelocity.y,
        //    number = GameManager.Instance.localPlayerId
        //};
        GameManager.Instance.SetInput(new PlayerInput()
        {
            forceX = releaseVelocity.x,
            forceY = releaseVelocity.y,
            number = GameManager.Instance.localPlayerId
        });
        //rb.AddForce(releaseVelocity, ForceMode2D.Impulse);
        //rb.AddTorque(paraboleData.rotate, ForceMode2D.Impulse);
        releaseVelocity = Vector2.zero;
        addForce = 0;
        line1.enabled = false;
        line2.enabled = false;
        dragPoint.SetActive(false);

        if (CompareTag("Bomb"))
        {
            GetComponent<Bomb>().throwing = true;
        }
    }

    //private void OnMouseDown()
    //{

    //}

    //private void OnMouseDrag()
    //{

    //    text.text = "OnMouseDrag + " + gameObject.name;
    //    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    addForce = Vector2.Distance(mousePosition, transform.position);
    //    addForce = Mathf.Clamp(addForce, 0, paraboleData.maxForce);

    //    line2Points[0] = transform.position;
    //    line2Points[1] = line2Points[0] + ((Vector3)mousePosition - line2Points[0]).normalized * addForce;
    //    line2.SetPositions(line2Points);

    //    dragPoint.transform.position = line2Points[1];
    //    for (int t = 0; t < paraboleData.line1Num; t++)
    //    {
    //        line1Points[t] = (Vector2)transform.position
    //            + (((Vector2)transform.position - mousePosition).normalized * addForce * paraboleData.fixedForce / rb.mass) * (t * paraboleData.pointsGap)
    //            + (Physics2D.gravity * paraboleData.gravityForce) * 0.5f * (t * paraboleData.pointsGap) * (t * paraboleData.pointsGap);
    //        //��ƽ���˶����������߶�Ϊhʱ�������������ٶȣ�����ٿ����˶���ʱ�䣬�������Ҫ������ͬ��λ�ã���ô��Ҫ���ӳ�ʼ�����������ʹﵽ�ˡ�������ͬλ�ã��ӿ��˶����̡���Ч����
    //    }

    //    line1.SetPositions(line1Points);
    //    releaseVelocity = ((Vector2)transform.position - mousePosition).normalized * addForce * paraboleData.fixedForce;
    //}

    //private void OnMouseUp()
    //{
    //    text.text = "OnMouseUp + " + gameObject.name;
    //    rb.AddForce(releaseVelocity,ForceMode2D.Impulse);
    //    rb.AddTorque(paraboleData.rotate, ForceMode2D.Impulse);

    //    releaseVelocity = Vector2.zero;
    //    addForce = 0;
    //    line1.enabled = false;
    //    line2.enabled = false;
    //    dragPoint.SetActive(false);
    //    if (CompareTag("Bomb"))
    //    {
    //        GetComponent<Bomb>().throwing = true;
    //    }


    //}





}
