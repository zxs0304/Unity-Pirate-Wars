using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bomb : MonoBehaviour
{
    public short minionNumber;
    private Rigidbody2D rb;
    public bool throwing = false;
    public float explodeRadius =8f;
    public float explodeForce = 10f;
    public Vector2 explodePosition ;
    public Vector2 testVector;
    public float damage = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (throwing)
        {
            //print("±¨’®¡À");
            OnExplode();
            explodePosition = transform.position;
        }

    }


    public void OnExplode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position ,explodeRadius );
        GameObject gb = Resources.Load<GameObject>("Explode");
        Instantiate(gb,transform.position,Quaternion.identity);
        foreach (Collider2D collider in colliders)
        {
            print("’®µΩ¡À " + collider.name + " " + collider.tag);

            if (collider.CompareTag("Player"))
            {
  
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                Minion minion = collider.GetComponent<Minion>();
                Animator animator = collider.GetComponent<Animator>();
                testVector = (collider.transform.position - transform.position).normalized;
                print("testVector " +  testVector);
                rb.AddForce(testVector * explodeForce , ForceMode2D.Impulse);
                rb.AddTorque(1.5f,ForceMode2D.Impulse);
                minion.SetHp(damage);
                animator.Play("Hurt");
            }
        }
        throwing = false;


        InputManager.Instance.canOperate = true;
        CameraManager.Instance.ActivateMovedCamera(transform);

        //Destroy(gameObject);

    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(explodePosition, explodeRadius);
    }
}
