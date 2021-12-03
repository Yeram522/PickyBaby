using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public bool getitem = false;
    GameObject player;
    public GameObject hand;
    GameObject pick;
    Player Player_s;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hand = GameObject.FindGameObjectWithTag("Hand");

        Player_s = player.GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && getitem == true)
        {
            Debug.Log("aa");
            transform.SetParent(hand.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);

            Player_s.pickItem(gameObject);
            getitem = false;
            Player_s.hasItem = true;
        }
    }

  
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        //������ ���� Ȯ��
        if (collision.transform.tag == "Player")
        {
            getitem = true;
        }
    }
}