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
    public Vector2 foceVector;
    public float maxDamage = 10f;
    public float maxDistance = 3f; //�����Եó�
    public float correctForceY = 0.5f;

    //TEST
    public float explodeforceCorrect = 1.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (throwing)
        {
            //print("��ը��");
            SoundManager.Instance.PlaySound(SoundManager.Instance.bang);
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
            print("ը���� " + collider.name + " " + collider.tag);

            if (collider.CompareTag("Player"))
            {
                Minion minion = collider.GetComponent<Minion>();
                foceVector = (collider.transform.position - transform.position).normalized;
                print("foceVector " + foceVector);
                if(foceVector.y > 0) //����y�����ϵı�ը��������һЩ
                {
                    foceVector.y = correctForceY;
                }


                //rb.AddForce(testVector * explodeForce , ForceMode2D.Impulse);
                //rb.AddTorque(1.5f,ForceMode2D.Impulse);

                float currentDistance = Vector2.Distance(collider.transform.position, transform.position);

                currentDistance = Mathf.Clamp(currentDistance, 0, 3f);
                float currentDamage = (-(maxDamage / maxDistance) * currentDistance) + maxDamage;

                minion.Jump(foceVector * explodeForce, 1.5f);

                print("����:" + currentDistance + "  �˺�:"+currentDamage);

                minion.SetHp(currentDamage);

            }
        }
        throwing = false;


        InputManager.Instance.canOperate = true;
        CameraManager.Instance.ActivateMovedCamera(transform);

        Destroy(gameObject);

    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(explodePosition, explodeRadius);
    }
}
