
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

        if (playerInput.number > 100) //����ը������,101��ʾ1��С������ը��
        {
            int index = playerInput.number - 101;
            minions[index].SpawnBomb();
            return;
        }

        Vector2 addForce = new Vector2(playerInput.forceX * forceAmount, playerInput.forceY * forceAmount);
        //Debug.Log($"��ǰclient{localId}, minions����{minions.Count} ,forceX :{playerInput.forceX} ,forceY{playerInput.forceY} ");

        if (playerInput.number > 0 && playerInput.number < 100)//ִ�������Ծ,1��ʾ1��С����Ծ��2��ʾ2��...
        {
            int index = playerInput.number - 1;
            minions[index].Jump(addForce);

            CameraManager.Instance.FixedCameraFollow(minions[index].transform);
            CameraManager.Instance.ActivateFixedCamera();

            //���˲�����Ҫ�л��غ�Ȼ�󲥶���(�ݶ�)
            InputManager.Instance.canOperate = false;
            GameManager.Instance.SwitchRound();

        }
        else if (playerInput.number < 0) //��ը������ -1��ʾ1��С����ը��...
        {
            int index = (-playerInput.number) - 1;
            minions[index].ThrowBomb(addForce);

            CameraManager.Instance.FixedCameraFollow(minions[index].currentBomb.transform);
            CameraManager.Instance.ActivateFixedCamera();

            //������ը���Ĳ�����Ҫ�л��غ�Ȼ�󲥶���
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
