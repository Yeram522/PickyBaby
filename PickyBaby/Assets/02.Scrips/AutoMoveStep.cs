using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveStep : MonoBehaviour
{
    public bool rotating = false;
    public float speed = 0.01f;
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
        if (!rotating) move();
        else
            rotationMove();


    }

    void rotationMove()
    {
        // The step size is equal to speed times frame time.
        var step = speed * Time.deltaTime;
        if (trigger == true)
        {

            Quaternion targetRotation = Quaternion.LookRotation(endpos.transform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3.0f);
            this.transform.position = Vector3.MoveTowards(this.transform.position, endpos.transform.position, 0.005f);


        }
        if (trigger == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(startpos.transform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3.0f);
            this.transform.position = Vector3.MoveTowards(this.transform.position, startpos.transform.position, 0.005f);
        }
        

                                                                                                                                                                                                                      
    }

    void move()//수직수평이동
    {
        if (trigger==true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, endpos.transform.position, speed);


        }
        if(trigger==false)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, startpos.transform.position, speed);
      
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
