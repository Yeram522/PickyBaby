using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage01Manager : MonoBehaviour
{
    public GameObject[] bombers = new GameObject[4];
    public GameObject portal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isClear())
        {
            portal.SetActive(true);
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
