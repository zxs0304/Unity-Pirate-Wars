
using Lockstep.Logic;
using System.Collections.Generic;
using UnityEngine;


public class User 
{
    public string name;
    public int Id;
    public int localId;
    public List<Minion> minions = new List<Minion>();
    public GameObject testMinion;
    private PlayerInput myInput;

    public float forceAmount = 5f;


    public void InitUser()
    {
        SpawnMinions();
    }

    public void HandleInput(PlayerInput playerInput)
    {
        if(playerInput.forceX == 0 && playerInput.forceY == 0)
        {
            return;
        }
        Vector2 addForce = new Vector2(playerInput.forceX * forceAmount, playerInput.forceY * forceAmount);

        minions[0].GetComponent<Rigidbody2D>().AddForce(addForce, ForceMode2D.Impulse);

    }

    private void SpawnMinions()
    {
        minions = new List<Minion>();
        GameObject gb = Resources.Load<GameObject>("Minion");
        GameObject.Instantiate(gb);
        gb.transform.position = new Vector2(Random.Range(-5,-8), -16);
        minions.Add(gb.GetComponent<Minion>());
    }


}
