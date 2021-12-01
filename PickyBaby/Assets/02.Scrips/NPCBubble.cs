using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBubble : MonoBehaviour
{
    public GameObject panel;
    private bool isEnter;
    private bool isCharge;
    private GameObject player;
    private void Update()
    {
        //if (isCharge) return;
        //if (isEnter && Input.GetKey(KeyCode.E))
        //{
        //    panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        //    panel.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        //    //player.GetComponent<Player>().hp = 100;
        //    isCharge = true;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
       // if (isCharge) return;
        if (other.CompareTag("Player"))
        {
           // player = other.gameObject;
            isEnter = true;
            panel.SetActive(true);
            panel.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit(Collider other)
    {
       // if (isCharge) return;
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false);
            isEnter = false;
        }
    }
}
