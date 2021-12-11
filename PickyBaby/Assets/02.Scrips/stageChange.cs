using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageChange : MonoBehaviour
{
    public bool OnPlayer = false;
    Vector3 startPoint;
    Vector3 endPoint;
    void Start()
    {
        startPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPoint = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >=-2.0f)
        {
            OnPlayer = false;
        }

        if((OnPlayer == true) && (transform.position.y != endPoint.y))
        {
            transform.Translate(Vector3.up * 3.0f * Time.deltaTime);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            OnPlayer = true;
        }

        if(collision.transform.tag =="Stop")
        {
            OnPlayer = false;
        }
    }

}

    
