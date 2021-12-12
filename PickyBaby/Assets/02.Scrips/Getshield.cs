using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getshield : MonoBehaviour
{
    public int Shield = 3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Debug.Log("ff");
            Shield -= 1;
            if (Shield == 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
