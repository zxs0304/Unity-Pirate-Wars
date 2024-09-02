using Lockstep.Collision2D;
using Lockstep.Logic;
using Lockstep.Math;
using LockstepTutorial;
using System;
using UnityEngine;

public class InputMono : MonoBehaviour
{
    private static bool IsReplay => GameManager.Instance.IsReplay;
    //[HideInInspector] public int floorMask;
    //public float camRayLength = 100;
    //public bool hasHitFloor;
    //public LVector2 mousePos;
    //public LVector2 inputUV;
    //public bool isInputFire;
    //public int skillId;
    //public bool isSpeedUp;
    public Vector2 Force;
    public float Torque;
    public PlayerInput myInput;

    void Start()
    {

    }

    public void Update()
    {
        if (!IsReplay)
        {





            //GameManager.CurGameInput = new PlayerInput()
            //{
            //    number = GameManager.Instance.localPlayerId
            //};

        }
    }

    public void GetMyInput()
    {

    }

}
