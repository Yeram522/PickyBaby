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
        Application.Quit();
    }

    public void go2mainBtn()
    {
        SceneManager.LoadScene("start");
    }

    public void retryBtn()
    {
        SceneManager.LoadScene("main01");
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
