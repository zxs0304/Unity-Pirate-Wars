using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool throwing = false;

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
            OnExplode();
        }


    }

    public void OnExplode()
    {
        Destroy(gameObject);
    }

}
