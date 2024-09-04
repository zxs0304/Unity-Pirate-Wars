
using Lockstep.Logic;
using LockstepTutorial;
using System.Collections.Generic;
using UnityEngine;


public class User 
{
    public string name;
    public int Id;
    public int localId;
    public List<Minion> minions = new List<Minion>();
    public GameObject testMinion;


    public float forceAmount = 1f;
    public float xuanzhuan = 3f;

    public void InitUser()
    {
        SpawnMinions();
    }

    public void HandleInput(PlayerInput playerInput)
    {
        if(playerInput.number == -999)
        {
            return;
        }

        if (playerInput.number > 100) //生成炸弹操作,101表示1号小人生成炸弹
        {
            int index = playerInput.number - 101;
            minions[index].SpawnBomb();
            return;
        }

        Vector2 addForce = new Vector2(playerInput.forceX * forceAmount, playerInput.forceY * forceAmount);
        //Debug.Log($"当前client{localId}, minions长度{minions.Count} ,forceX :{playerInput.forceX} ,forceY{playerInput.forceY} ");

        if (playerInput.number > 0 && playerInput.number < 100)//执行玩家跳跃,1表示1号小人跳跃，2表示2号...
        {
            int index = playerInput.number - 1;
            minions[index].Jump(addForce);

            CameraManager.Instance.FixedCameraFollow(minions[index].transform);
            CameraManager.Instance.ActivateFixedCamera();

            //做了操作就要切换回合然后播动画(暂定)
            InputManager.Instance.canOperate = false;
            GameManager.Instance.SwitchRound();

        }
        else if (playerInput.number < 0) //扔炸弹操作 -1表示1号小人扔炸弹...
        {
            int index = (-playerInput.number) - 1;
            minions[index].ThrowBomb(addForce);

            CameraManager.Instance.FixedCameraFollow(minions[index].currentBomb.transform);
            CameraManager.Instance.ActivateFixedCamera();

            //做了扔炸弹的操作就要切换回合然后播动画
            InputManager.Instance.canOperate = false;
            GameManager.Instance.SwitchRound();
        }

        //minions[0].GetComponent<Rigidbody2D>().AddForce(addForce, ForceMode2D.Impulse);
        //minions[0].GetComponent<Rigidbody2D>().AddTorque(GameManager.Instance.Testxuanzhuan * -addForce.normalized.x, ForceMode2D.Impulse);

    }

    private void SpawnMinions()
    {
        string prefabName = "Minion" + localId;
        int Count = GameManager.Instance.minionSpawnPoint.Length;

        for(int i = localId; i < Count; i += 2)
        {
            GameObject minion = Resources.Load<GameObject>(prefabName);
            GameObject gb = GameObject.Instantiate(minion);
            gb.transform.position = GameManager.Instance.minionSpawnPoint[i].position;
            gb.name = localId.ToString();
            Minion m = gb.GetComponent<Minion>();
            m.userId = localId;
            minions.Add(m);

        }

       

    }

    

}
