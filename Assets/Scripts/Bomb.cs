using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool throwing = false;
    public float explodeRadius = 1f;
    public float explodeForce = 10f;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (throwing)
        {
            print("��ը��");
            OnExplode();
        }


    }

    public void OnExplode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position ,explodeRadius );

        foreach (Collider2D collider in colliders)
        {
            print("ը���� " + collider.name + " " +collider.tag);
            
            if (collider.CompareTag("Player"))
            {
                print("�����������");
                collider.GetComponent<Rigidbody2D>().AddForce( (collider.GetComponent<Transform>().position - transform.position).normalized * explodeForce);
            }
        }
        throwing = false;
        //Destroy(gameObject);
    }

}
