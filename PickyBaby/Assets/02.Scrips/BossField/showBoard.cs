using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showBoard : MonoBehaviour
{
    public GameObject Boss;
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
            StartCoroutine(showUpBoss());
            
            
        }
    }

    private IEnumerator showUpBoss()
    {
        Boss.SetActive(true);
        yield return null;
    }
}
