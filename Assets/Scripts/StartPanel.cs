using LockstepTutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public Button button;
    public Text content;
    public Coroutine coroutine;

    private void Start()
    {
        button.onClick.AddListener(() => 
        {
            GameManager.Instance._Start();
            coroutine =  StartCoroutine(StartMatchingText());
        });
    }

    public IEnumerator StartMatchingText()
    {
        //int maxDotCount = 3;
        string dot = "...";

        while (true)
        {
            if(dot == "")
            {
                dot = ".";
            }
            else if(dot == ".")
            {
                dot = "..";
            }
            else if(dot == "..")
            {
                dot = "...";
            }
            else if (dot == "...")
            {
                dot = "";
            }
            content.text = "Matching" + dot;
            yield return new WaitForSeconds(0.5f);

        }


    }

    public void ReadyToStartGame()
    {
        StartCoroutine(ReadyToStart());
    }
    public IEnumerator ReadyToStart()
    {
        StopCoroutine(coroutine);
        content.text = "Successful !";
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.PlayBgm();
        GameManager.Instance.roundText.gameObject.SetActive(true);
        gameObject.SetActive(false);


    }


}
