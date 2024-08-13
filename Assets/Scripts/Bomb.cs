using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool throwing = false;
    public float explodeRadius =8f;
    public float explodeForce = 10f;
    public Vector2 explodePosition ;
    public Vector2 testVector;
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
            explodePosition = transform.position;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    public void OnExplode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position ,explodeRadius );

        foreach (Collider2D collider in colliders)
        {
            print("ը���� " + collider.name + " " +collider.tag);
            
            if (collider.CompareTag("Player"))
            {
  
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                testVector = (collider.transform.position - transform.position).normalized;
                rb.AddForce(testVector * explodeForce , ForceMode2D.Impulse);
                print("�����������" + rb.name );
            }
        }
        throwing = false;
        //Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(explodePosition, explodeRadius);
    }
}
