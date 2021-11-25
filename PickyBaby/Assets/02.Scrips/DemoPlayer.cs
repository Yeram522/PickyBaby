using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayer : MonoBehaviour
{
    public float jumpPower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
          
        }

        //이동 및 회전
        if (Input.GetKeyDown(KeyCode.A))
        {
            float rnd = Random.Range(0.0f, 5.0f);//0.0f~5.0f사이의 난수
            this.transform.position = new Vector3(0.0f, 0.5f, rnd);//자신(Capsule)의 위치를 변경
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            float rnd = Random.Range(0.0f, 360.0f);
            this.transform.rotation = Quaternion.Euler(0.0f, rnd, 0.0f);
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward * 3.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back * 3.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * 3.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * 3.0f * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.R))
        {
            this.transform.Rotate(0.0f, 90.0f * Time.deltaTime, 0.0f);
        }

        if (Input.GetKey(KeyCode.L))
        {
            this.transform.Rotate(0.0f, -90.0f * Time.deltaTime, 0.0f);
        }

    }
}
