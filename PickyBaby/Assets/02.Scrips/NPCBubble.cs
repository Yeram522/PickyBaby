using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBubble : MonoBehaviour
{
    public GameObject panel;
    private bool isEnter;
    private bool isCharge;
    private GameObject player;
    private GameObject unactivePanel;

    private void Start()
    {
        unactivePanel = panel.transform.GetChild(0).GetChild(1).gameObject;
    }
    private void Update()
    {
        if (isCharge) return;
        if (isEnter && Input.GetKey(KeyCode.E))
        {
            panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            panel.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            panel.transform.GetChild(1).gameObject.SetActive(false);
            unactivePanel = panel.transform.GetChild(0).GetChild(0).gameObject;
            if(player != null) player.GetComponent<Player>().HP = 1.0f;
            isCharge = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            isEnter = true;
            panel.SetActive(true);
            unactivePanel.SetActive(false);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false);
            isEnter = false;
        }
    }
}
