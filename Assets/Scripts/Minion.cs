using LockstepTutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public int minionId;
    public float maxHP;
    public float currentHP;
    public float correctionFactor = 5f;
    private Rigidbody2D rb;
    public GameObject bomb;
    public GameObject currentBomb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    public void SpawnBomb()
    {
        GameObject gb =  Instantiate(bomb);
        gb.transform.position = transform.position;
        currentBomb = gb;
    }

    public void ThrowBomb(Vector2 force)
    {
        if(currentBomb != null)
        {
            Rigidbody2D bombRb =  currentBomb.GetComponent<Rigidbody2D>();
            bombRb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public void Jump(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        rb.AddTorque(3f * -force.normalized.x, ForceMode2D.Impulse);
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        float targetRotation = 0f; // Ä¿±ê½Ç¶È
        rb.rotation = Mathf.LerpAngle(rb.rotation, targetRotation, Time.deltaTime * correctionFactor);
    }

}
