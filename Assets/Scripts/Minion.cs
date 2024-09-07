using LockstepTutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Minion : MonoBehaviour
{
    public int userId = 0; //����������ҵ�localId
    public short minionNumber = 1;
    public float maxHP = 100f;
    public float currentHP;
    public float correctionFactor = 5f;
    private Rigidbody2D rb;
    public GameObject bomb;
    public Bomb currentBomb;
    public Slider HpBar;
    public bool isGround;
    public bool wasGround;

    //TEST
    public float jumpEndTime = 2.5f;
    public float footOffset = 1f;
    public float checkGroundLength = 0.2f;
    public float forceSetAngle = 30;
    public float torqueVelocity = 0.2f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;


    }

    public void ThrowBomb(Vector2 force)
    {
        if (currentBomb != null)
        {
            Rigidbody2D rb = currentBomb.GetComponent<Rigidbody2D>();
            rb.AddForce(force,ForceMode2D.Impulse);
            currentBomb.throwing = true;


        }

    }
    public void SpawnBomb()
    {
        GameObject gb =  Instantiate(bomb);
        gb.transform.position = transform.position;
        currentBomb = gb.GetComponent<Bomb>();
        currentBomb.minionNumber = minionNumber ;
        

    }

    public void Jump(Vector2 force)
    {

        HpBar.gameObject.SetActive(false);
        rb.AddForce(force, ForceMode2D.Impulse);
        rb.AddTorque(-3f * -force.normalized.x, ForceMode2D.Impulse);

    }

    
    public void JumpEnd()
    {
        HpBar.gameObject.SetActive(true);
        InputManager.Instance.canOperate = true;
        CameraManager.Instance.ActivateMovedCamera(transform);
        print("�����");
    }

    public void SetHp(float damage)
    {
        currentHP -= damage;
        if (currentHP < 0)
        {
            HpBar.value = 0;
            Dead();
        }
        else
        {
            HpBar.value = currentHP / maxHP;
        }

        
    }

    private void Dead()
    {

    }

    private void Update()
    {

        wasGround = isGround;
        isGround = Physics2D.Raycast((Vector2)transform.position - new Vector2(0,footOffset), Vector2.down, checkGroundLength ,LayerMask.GetMask("Ground"));

        // ���ӿ�����ص���һ֡
        if (isGround && !wasGround)
        {
            JumpEnd();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision) //�������ʱ�ķ���
    {
        rb.angularVelocity = rb.angularVelocity * torqueVelocity;
        float targetRotation = 0f; // Ŀ��Ƕ�
        rb.rotation = Mathf.LerpAngle(rb.rotation, targetRotation, correctionFactor);
        if (rb.rotation < forceSetAngle || rb.rotation > -forceSetAngle)
        {
            rb.rotation = 0;
        }
    }

    //private void OnCollisionStay2D(Collision2D collision) //�������ʱ�ķ���
    //{


    //    float targetRotation = 0f; // Ŀ��Ƕ�
    //    rb.rotation = Mathf.LerpAngle(rb.rotation, targetRotation,  correctionFactor);
    //    if(rb.rotation < forceSetAngle || rb.rotation > -forceSetAngle)
    //    {
    //        rb.rotation = 0;
    //    }
        
    //    print("StayROTATION " + rb.rotation);
    //}

    private void OnDrawGizmos()
    {
        // ���ӻ� Raycast
        Gizmos.color = Color.red;
        Gizmos.DrawLine((Vector2)transform.position - new Vector2(0, footOffset), (Vector2)transform.position - new Vector2(0, footOffset) + Vector2.down * checkGroundLength);
    }

}
