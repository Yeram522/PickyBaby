using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class item : MonoBehaviour
{
    GameObject player;
    public GameObject hand;
    GameObject pick;
    Player Player_s;
    public bool getitem = false;

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
            transform.SetParent(hand.transform);
            this.transform.localPosition = Vector3.one;
            
            transform.rotation = new Quaternion(0, 0, 0, 0);

            Player_s.pickItem(this.gameObject);
            getitem = false;
        }

    }

    IEnumerator waitTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void OnCollisionEnter(Collision collision)
    {
        //아이템 접촉 확인
        if (collision.transform.tag == "Player")
        {
            getitem = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            getitem = false;
        }

    }
}
