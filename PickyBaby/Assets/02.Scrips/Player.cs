using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject playerStatusUI;
    public float jumpPower;
    public bool isJump = false;
    public float HP;
    public GameObject Hand;
    public GameObject pick;
    public bool getitem = false;// �����۰� ������ ��������?
    public bool hasItem = false; //�տ� �������� �ִ���?
    public bool isAimming = false;

    private Slider uiHp;
    Animator animator;

    public float power;
    // Start is called before the first frame update
    void Start()
    {
        //playerStatusUI = this.transform.Find("PlayerHP").gameObject;
        HP = 1.0f;
        uiHp = playerStatusUI.transform.GetChild(0).GetComponent<Slider>();
        Hand = GameObject.FindGameObjectWithTag("Hand");
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        uiHp.value = HP;
        // ����
        Jump();
 
        //�̵� �� ȸ��
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * 4.0f * Time.deltaTime);
            animator.SetBool("Run", true);
        }
        else 
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * 4.0f * Time.deltaTime);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * 4.0f * Time.deltaTime);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * 4.0f * Time.deltaTime);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }


        if (Input.GetMouseButtonDown(0))
        {
            this.transform.Rotate(0.0f, 30.0f, 0.0f);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            this.transform.Rotate(0.0f, -30.0f, 0.0f);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        // ������
        if (Input.GetKey(KeyCode.E) &&  hasItem )
        {
            StartCoroutine(throwItem());
        }
    }

    IEnumerator throwItem()
    {
        animator.SetTrigger("throw");
        yield return new WaitForSeconds(0.8f);
        dropItem();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
            isJump = true;
            animator.SetTrigger("Jump");

        }
    }

    public void pickItem(GameObject pick)
    {
        Setitem(pick, true);
        hasItem = true;
        animator.SetTrigger("Pick");
    }

    public IEnumerator pickIt(GameObject pick)
    {
        animator.SetTrigger("Pick");
        yield return new WaitForSeconds(5.0f);
        pickItem(pick);
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

        Ray ray = new Ray(transform.position, transform.forward); //ray ĳ���Ͱ� �ٶ󺸴� �������� ���
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
            throwAngle = transform.forward * power; // ���ư��� �Ÿ�
        }

    
        throwAngle.y = 5f; //���������� ���ư��� ���� �ߴ� ����
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
        //2�� ���� ����(�⺻)
        if (collision.transform.tag == "Floor")// ���� ��� Floor
        {
            isJump = false;
        }
    }
}
