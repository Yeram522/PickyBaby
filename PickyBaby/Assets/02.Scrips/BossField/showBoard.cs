using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showBoard : MonoBehaviour
{
    public GameObject UImanager;
    private bool trigger;

    private void Start()
    {
        trigger = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (trigger) return;
        if (other.CompareTag("Player"))
        {
            UImanager.GetComponent<map2UImanager>().showUIBoard2();
            trigger = true;
        }
    }
}
