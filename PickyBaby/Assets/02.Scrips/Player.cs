using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower;
    public bool isJump = false;
    public GameObject Hand;
    public GameObject pick;
    public bool getitem = false;// 아이템과 접촉한 상태인지?
    public bool hasItem = false; //손에 아이템이 있는지?
    public bool isAimming = false;

    public float power;
    // Start is called before the first frame update
    void Start()
    {
        Hand = GameObject.FindGameObjectWithTag("Hand");
    }

    // Update is called once per frame
    void Update()
    {
        // 점프
        Jump();

        //이동 및 회전
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * 3.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * 3.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * 3.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * 3.0f * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.transform.Rotate(0.0f, 30.0f, 0.0f);
        }

        if (Input.GetMouseButtonDown(1))
        {
            this.transform.Rotate(0.0f, -30.0f, 0.0f);
        }
        
        // 던지기
        if (Input.GetKey(KeyCode.E) &&  hasItem )
        {
            Debug.Log("dd");
            dropItem();
        }


    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
            isJump = true;

        }
    }

    public void pickItem(GameObject pick)
    {
        Setitem(pick, true);
        hasItem = true;
    }

  
   
    void dropItem()
    {
       /*
        pick = Hand.GetComponentInChildren<Rigidbody>().gameObject;
        Setitem(pick, false);
  
        Hand.transform.DetachChildren();
        hasItem = false;
        */
     
        pick = Hand.GetComponentInChildren<Rigidbody>().gameObject;
        Rigidbody pick_rigidbody = pick.GetComponent<Rigidbody> ();
        
        Setitem(pick, false);
        Hand.transform.DetachChildren();

        Ray ray = new Ray(transform.position, transform.forward); //ray 캐릭터가 바라보는 방향으로 쏘기
        RaycastHit rayhit;
        float rayLength = 5f;
        int floorMask = LayerMask.GetMask("Floor");
        Vector3 throwAngle;

        if(Physics.Raycast(ray, out rayhit, rayLength,floorMask))
        {
            throwAngle = rayhit.point - Hand.transform.position;
        }

        else
        {
            throwAngle = transform.forward * power; // 날아가는 거리
        }

    
        throwAngle.y = 5f; //포물선으로 날아갈때 위로 뜨는 각도
        pick_rigidbody.AddForce(throwAngle * 1, ForceMode.Impulse);

        hasItem = false;

    }

    void Setitem(GameObject pick, bool getitem)
    {
        Debug.Log(!getitem);
        Collider[] pickColliders = pick.GetComponents<Collider>();
        Rigidbody pickRigidbody = pick.GetComponent<Rigidbody>();

        foreach (Collider pickCollider in pickColliders)
        {
            pickCollider.enabled = !getitem;
        }

        pickRigidbody.isKinematic = getitem;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //2단 점프 방지(기본)
        if (collision.transform.tag == "Floor")// 지형 모두 Floor
        {
            isJump = false;
        }
    }
}
