using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage01Manager : MonoBehaviour
{
    public GameObject[] bombers = new GameObject[4];
    public GameObject[] spawner = new GameObject[4];

    public GameObject portal;
    public GameObject board;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (portal.activeSelf) return;
        if(isClear())
        {
            portal.SetActive(true);
            board.GetComponent<map1UIManager>().showUIBoard2();
            for (int i = 0; i < spawner.Length; i++)
            {
                spawner[i].SetActive(false);
            }
        }
    }

    bool isClear()
    {
        for(int i = 0; i<bombers.Length;i++)
        {
            if (bombers[i].GetComponent<StaticEnemyBehavior>().count != 2) return false;
        }
        return true;
    }
}
