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
    //이단 점프 - 마우스 클릭시 사용 가능하도록 하면 됨.
    public bool doublejump = false;
    int jumppoint = 2;
    //사망시 체력 30으로 부활
    public bool lifeBack = false;
    //방어막 3회
    public bool isShield = false;
    int Shield = 3;

    //UI
    public GameObject playerStatusUI;
    public float HP;
    private Slider uiHp;

    //player 기능
    float speed = 4.0f;
    public float jumpPower;
    public bool isJump = false;
    public float power;
    public GameObject Hand;
    public GameObject pick;
    public bool getitem = false;// 아이템과 접촉한 상태인지?
    public bool hasItem = false; //손에 아이템이 있는지?
    public bool isAimming = false;
    Animator animator;

    //Audio
    private AudioSource audio;
    public AudioClip jumpsound;
    public AudioClip throwsound;
    public AudioClip damagedsound;
    public AudioClip getItemsound;

    void Start()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();      
        this.audio.loop = false;

        if (SceneManager.GetActiveScene().name == "main02")
        {
            var obj = FindObjectOfType<DonDestroyObject>();
            HP = obj.GetComponent<DonDestroyObject>().PlayerHp;
        }

        uiHp = playerStatusUI.transform.GetChild(0).GetComponent<Slider>();
        Hand = GameObject.FindGameObjectWithTag("Hand");
        animator = GetComponentInChildren<Animator>();
        uptime = 0;
        downtime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(uiHp.value > HP)
        {
            this.audio.clip = this.damagedsound;
            this.audio.Play();
        }
        uiHp.value = HP;

        //0이면 fail
        if (HP <= 0 && lifeBack == false)
        {
            if (SceneManager.GetActiveScene().name == "main01")
            {
                SceneManager.LoadScene("fail");
            }
            if (SceneManager.GetActiveScene().name == "main02")
            {
                SceneManager.LoadScene("fail1");
            }
        }

        // 점프
        Jump();

        //이동
        Move();

        //체력+10 아이템
        if(hpup == true && isItem == true)
        {
            HP += 0.1f;
            hpup = false;
            isItem = false;
        }

        //사망시 30 체력으로 부활 아이템 (즉사는 해당 안됨.)
        if(lifeBack == true && HP <= 0.0f)
        {
            HP += 0.3f;
            lifeBack = false;
        }

        //방어막 3회
        if (isShield == true)
        {
            this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.transform.Rotate(0.0f, 30.0f, 0.0f);
           
        }

        if (Input.GetMouseButtonDown(1))
        {
            this.transform.Rotate(0.0f, -30.0f, 0.0f);
            
        }
     
        // 던지기 및 줍기
        if (Input.GetKeyDown(KeyCode.LeftControl) &&  hasItem == true )
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
        
        //더블 점프
        if (doublejump == true)
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
                this.audio.clip = this.jumpsound;
                this.audio.Play();
            }
        }

        //기본 점프
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJump)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
                isJump = true;
                animator.SetTrigger("Jump");
                this.audio.clip = this.jumpsound;
                this.audio.Play();
            }
        }
    }

    void Move()
    {
        //스피드 업 아이템 획득시
        if (speedup == true && isItem == true)
        {
            uptime += Time.deltaTime;
            speed = 10.0f;

            //이동 및 회전
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
        // 스피드 다운 아이템 적용시
        else if (speeddown == true && isItem == true)
        {
            downtime += Time.deltaTime;
            speed = 2.0f;

            //이동 및 회전
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

        // 평상시 이동 및 회전
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
        animator.SetTrigger("Pick");
        hasItem = true;
    }


    void dropItem()
    {
        this.audio.clip = this.throwsound;
        this.audio.Play();

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
        animator.SetTrigger("throw");
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
        //2단 점프 방지(기본)
        if (collision.transform.tag == "Floor")// 지형 모두 Floor
        {
            isJump = false;
        }


        if(collision.transform.tag == "fall")
        {
            if (SceneManager.GetActiveScene().name == "main01")
            {
                SceneManager.LoadScene("fail");
            }
            if (SceneManager.GetActiveScene().name == "main02")
            {
                SceneManager.LoadScene("fail1");
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //스피드업 아이템
        if (other.tag == "speedUP" && isItem == false)
        {
            this.audio.clip = this.getItemsound;
            this.audio.Play();
            other.gameObject.SetActive(false);
            isItem = true;
            speedup = true;
        }

        //스피드다운 아이템
        if (other.tag == "speedDown" && isItem == false)
        {
            this.audio.clip = this.getItemsound;
            this.audio.Play();
            other.gameObject.SetActive(false);
            isItem = true;
            speeddown = true;
        }

        //체력 +10 아이템
        if (other.tag == "HpUp" && isItem == false)
        {
            this.audio.clip = this.getItemsound;
            this.audio.Play();
            other.gameObject.SetActive(false);
            isItem = true;
            hpup = true;
        }
    }


    public void ClickJump()
    {
        doublejump = true;
    }
    public void ClickAngle()
    {
        lifeBack = true;
    }

    public void ClickShield()
    {
        isShield = true;
    }

}