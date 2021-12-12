using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getshield : MonoBehaviour
{
    public int Shield = 3;
    private bool isactive;
    void Start()
    {
        isactive = false;
    }

    IEnumerator isBeingShield()
    {
        if (Shield == 0)
        {
            transform.parent.GetComponent<Player>().isShield = false;
            this.gameObject.SetActive(false);
            yield return null;
        }

        Shield -= 1;
        isactive = true;
        yield return new WaitForSeconds(4.0f);
        isactive = false;
        yield return null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy" && 
            !isactive)
        {
            Debug.Log("¹æ¾î!");
            if (!this.gameObject.activeSelf) return;
            StartCoroutine(isBeingShield());
            
        }
    }
}
