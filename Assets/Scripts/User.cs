
using System.Collections.Generic;
using UnityEngine;

public class User 
{
    public string name;
    public int Id;
    public int localId;
    public List<Minion> minions = new List<Minion>();
    public GameObject testMinion;
    public void InitUser()
    {
        SpawnMinions();
    }


    private void SpawnMinions()
    {
        GameObject gb = Resources.Load<GameObject>("Prefabs/Player");
         //GameObject.Instantiate(testMinion);
        gb.transform.position = new Vector2(-8,-16);
    }


}
