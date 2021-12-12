using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneUI : MonoBehaviour
{
    public GameObject panel;

    //페이지 할당.
    public GameObject[] pages= new GameObject[4];

    [SerializeField]
    private int currentPg = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentPg = 0;
    }


    //button event
    public void openHowToPanel()
    {
        panel.SetActive(true);
    }

    public void closeHowToPanel()
    {
        pages[currentPg].SetActive(false);
        currentPg = 0;
        pages[currentPg].SetActive(true);
        panel.SetActive(false);
    }

    public void movePrevPg()
    {
        pages[currentPg].SetActive(false);
        currentPg--;
        if (currentPg == -1) currentPg = 3;
        pages[currentPg].SetActive(true);
        

    }

    public void moveNextPg()
    {
        pages[currentPg].SetActive(false);
        currentPg++;
        if (currentPg == 4) currentPg = 0;
        pages[currentPg].SetActive(true);
    }
}
