using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower;
    public bool isJump = false;
    public GameObject Hand;
    GameObject pick;
    public bool getitem = false;// �����۰� ������ ��������?
    public bool hasItem = false; //�տ� �������� �ִ���?


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ����
        Jump();

        //�̵� �� ȸ��
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
        
        // ������
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
       
        pick = Hand.GetComponentInChildren<Rigidbody>().gameObject;
        Setitem(pick, false);
        Debug.Log("�Ӥ�");
        Hand.transform.DetachChildren();
        hasItem = false;
    }

    void Setitem(GameObject pick, bool getitem)
    {
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
        //2�� ���� ����(�⺻)
        if (collision.transform.tag == "Floor")// ���� ��� Floor
        {
            isJump = false;
        }
    }
}
