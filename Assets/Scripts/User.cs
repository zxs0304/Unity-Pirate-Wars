
using Lockstep.Logic;
using System.Collections.Generic;
using UnityEngine;


public class User 
{
    public string name;
    public int Id;
    public int localId;
    public List<GameObject> minions = new List<GameObject>();
    public GameObject testMinion;


    public float forceAmount = 1f;
    public float xuanzhuan = 1f;

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
        //Debug.Log($"当前client{localId}, minions长度{minions.Count} ,forceX :{playerInput.forceX} ,forceY{playerInput.forceY} ");
        //Debug.Log("client : "+localId + " minions[0].transform" + minions[0].transform.position);

        minions[0].GetComponent<Rigidbody2D>().AddForce(addForce, ForceMode2D.Impulse);
        minions[0].GetComponent<Rigidbody2D>().AddTorque(xuanzhuan, ForceMode2D.Impulse);

    }

    private void SpawnMinions()
    {
        minions = new List<GameObject>();
        GameObject minion = Resources.Load<GameObject>("Minion");
        GameObject gb = GameObject.Instantiate(minion);
        gb.transform.position = new Vector2(Random.Range(-8.5f,-7), -16);
        gb.name = localId.ToString() ;
        minions.Add(gb);
        Debug.Log("minions[0].transform" + minions[0].transform.position);
    }

    

}
