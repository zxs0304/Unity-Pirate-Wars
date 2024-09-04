using Lockstep.Logic;
using LockstepTutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoSingleton<MenuPanel>
{
    public GameObject bluePanel;
    public GameObject redPanel;

    public Button jumpBtn1;
    public Button jumpBtn2;
    public Button BombBtn1;
    public Button BombBtn2;

    short currentMinionNumber;

    private void Start()
    {
        jumpBtn1.onClick.AddListener(OnClickJumpBtn);
        jumpBtn2.onClick.AddListener(OnClickJumpBtn);
        BombBtn1.onClick.AddListener (OnClickBombBtn);
        BombBtn2.onClick.AddListener(OnClickBombBtn);
    }

    public void OnClickJumpBtn()
    {
        InputManager.Instance.canDrag = true;
        HidePanel();
    }
    public void OnClickBombBtn()
    {
        PlayerInput input = new PlayerInput { number = (short)(currentMinionNumber + 100) }; //生成炸弹的操作码
        GameManager.Instance.SetInput(input);
        InputManager.Instance.canDrag = true;
        HidePanel();
    }

    public void ShowPanel(short minionNumber)
    {
        currentMinionNumber = minionNumber;
        if (GameManager.Instance.localPlayerId == 0)
        {
            redPanel.SetActive(true);
        }
        else
        {
            bluePanel.SetActive(true);
        }

    }

    public void HidePanel()
    {
        if (GameManager.Instance.localPlayerId == 0)
        {
            redPanel.SetActive(false);
        }
        else
        {
            bluePanel.SetActive(false);
        }
    }

    public void ResetBtn()
    {

    }

}
