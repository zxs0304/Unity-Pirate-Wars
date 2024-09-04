using LockstepTutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Minion : MonoBehaviour
{
    public int userId = 0; //所归属的玩家的localId
    public short minionNumber = 1;
    public float maxHP;
    public float currentHP;
    public float correctionFactor = 5f;
    private Rigidbody2D rb;
    public GameObject bomb;
    public Bomb currentBomb;

    //TEST
    public float jumpEndTime = 2.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();


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


        rb.AddForce(force, ForceMode2D.Impulse);
        rb.AddTorque(-3f * -force.normalized.x, ForceMode2D.Impulse);

        Invoke(nameof(JumpEnd), jumpEndTime);
    }

    //暂定
    public void JumpEnd()
    {
        InputManager.Instance.canOperate = true;
        CameraManager.Instance.ActivateMovedCamera(transform);
    }

    private void OnCollisionEnter2D(Collision2D collision) //矫正落地时的方向
    {
        float targetRotation = 0f; // 目标角度
        rb.rotation = Mathf.LerpAngle(rb.rotation, targetRotation, Time.deltaTime * correctionFactor);
    }

}
