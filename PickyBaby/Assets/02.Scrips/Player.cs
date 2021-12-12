using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //item
    bool isItem = false;
    bool speedup = false;
    bool speeddown = false;
    bool hpup = false;
    float uptime;
    float downtime;

    //skill
    //�̴� ���� - ���콺 Ŭ���� ��� �����ϵ��� �ϸ� ��.
    public bool doublejump = false;
    int jumppoint = 2;
    //����� ü�� 30���� ��Ȱ
    public bool lifeBack = false;
    //�� 3ȸ
    public bool isShield = false;
    int Shield = 3;

    //UI
    public GameObject playerStatusUI;
    public float HP;
    private Slider uiHp;

    //player ���
    float speed = 4.0f;
    public float jumpPower;
    public bool isJump = false;
    public float power;
    public GameObject Hand;
    public GameObject pick;
    public bool getitem = false;// �����۰� ������ ��������?
    public bool hasItem = false; //�տ� �������� �ִ���?
    public bool isAimming = false;
    Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        //playerStatusUI = this.transform.Find("PlayerHP").gameObject;
        HP = 1.0f;
        uiHp = playerStatusUI.transform.GetChild(0).GetComponent<Slider>();
        Hand = GameObject.FindGameObjectWithTag("Hand");
        animator = GetComponentInChildren<Animator>();
        uptime = 0;
        downtime = 0;

        if(isShield == true)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        uiHp.value = HP;
        // ����
        Jump();

        //�̵�
        Move();

        //ü��+10 ������
        if(hpup == true && isItem == true)
        {
            HP += 0.1f;
            hpup = false;
            isItem = false;
        }

        //����� 30 ü������ ��Ȱ ������ (���� �ش� �ȵ�.)
        if(lifeBack == true && HP <= 0.0f)
        {
            HP += 0.3f;
            lifeBack = false;
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
        //���� ����
        if(doublejump == true)
        {
            if(jumppoint == 0)
            {
                isJump = true;
                jumppoint = 2;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isJump)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
                jumppoint -= 1;
                animator.SetTrigger("Jump");
            }
        }

        //�⺻ ����
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJump)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
                isJump = true;
                animator.SetTrigger("Jump");
            }
        }
    }

    void Move()
    {
        //���ǵ� �� ������ ȹ���
        if (speedup == true && isItem == true)
        {
            uptime += Time.deltaTime;
            speed = 10.0f;

            //�̵� �� ȸ��
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }


            if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(Vector3.back * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(Vector3.left * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }


            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.right * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) animator.SetBool("isRunning", false);

            if (uptime > 3f)
            {
                speedup = false;
                speed = 4.0f;
                isItem = false;
                uptime = 0;
            }
        }
        // ���ǵ� �ٿ� ������ �����
        else if (speeddown == true && isItem == true)
        {
            downtime += Time.deltaTime;
            speed = 2.0f;

            //�̵� �� ȸ��
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }


            if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(Vector3.back * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(Vector3.left * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }


            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.right * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) animator.SetBool("isRunning", false);

            if (downtime > 3f)
            {
                speeddown = false;
                speed = 4.0f;
                isItem = false;
                downtime = 0;
            }
        }

        // ���� �̵� �� ȸ��
        else
        {
            
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }


            if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(Vector3.back * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(Vector3.left * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }


            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.right * speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) animator.SetBool("isRunning", false);

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

        //���ǵ�� ������
        if(collision.transform.tag == "speedUP" && isItem == false)
        {
            collision.gameObject.SetActive(false);
            isItem = true;
            speedup = true;
        }

        //���ǵ�ٿ� ������
        if (collision.transform.tag == "speedDown" && isItem == false)
        {
            collision.gameObject.SetActive(false);
            isItem = true;
            speeddown = true;
        }

        //ü�� +10 ������
        if (collision.transform.tag == "HpUp" && isItem == false)
        {
            collision.gameObject.SetActive(false);
            isItem = true;
            hpup = true;
        }

        if(collision.transform.tag == "fall")
        {
            SceneManager.LoadScene("fail");
        }
    }
}

//���� �����ϸ� �ݶ��̴� �ٱ��� ����. 
//�װ� 3�� �浹�ϰ� ���� �����.
