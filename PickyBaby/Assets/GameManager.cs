using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public  GameObject UIPanel;
    private GameObject OptionBtn;
    void Start()
    {
       // UIPanel = GameObject.Find("IngameUI/Panel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Option Buttons Behavior
    public void exitBtn()
    {
      // GameObject btn = GameObject.Find("IngameUI/Panel/exitBtn").gameObject;
        //게임 종료
    }

    public void go2mainBtn()
    {
       // GameObject btn = GameObject.Find("IngameUI/Panel/go2mainBtn").gameObject;
        //go to main scene
    }

    public void retryBtn()
    {
       // GameObject btn = GameObject.Find("IngameUI/Panel/retryBtn").gameObject;
        //go to main01 scene
    }

    public void closeTabBtn()
    {
        UIPanel.SetActive(false);

    }

    public void openTabBtn()
    {
        UIPanel.SetActive(true);
    }
}
