using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveStep : MonoBehaviour
{
    [SerializeField]
    private GameObject startpos;
    [SerializeField]
    private GameObject endpos;

    private bool trigger;
  

    void Start()
    {
        
        startpos = transform.parent.transform.Find("start").gameObject;
        endpos = transform.parent.Find("end").gameObject;
        trigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        move();

    }

   

    void move()//수직이동
    {
        if (trigger==true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, endpos.transform.position, 0.01f);


        }
        if(trigger==false)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, startpos.transform.position, 0.01f);
      
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("start")) trigger = true;
        if (other.CompareTag("end")) trigger = false;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) collision.collider.transform.SetParent(null);
    }
}
