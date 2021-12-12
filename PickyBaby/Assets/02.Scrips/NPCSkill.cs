using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSkill : MonoBehaviour
{
    public GameObject panel;
    GameObject skillList;
    GameObject player;
    bool isEnter;
    bool isCharge;
    // Start is called before the first frame update
    private void Start()
    {
        skillList = GameObject.Find("UI").transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (isCharge) return;
        if (isEnter && Input.GetKey(KeyCode.E))
        {
            skillList.SetActive(true);
            player.GetComponent<Player>().enabled = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag=="Player")
        {
            player = collision.gameObject;
            isEnter = true;
          
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            isEnter = false;

        }
    }

    public void OnClick()
    {
        player.GetComponent<Player>().enabled = true;
        skillList.SetActive(false);
        isCharge = true;
    }
}
