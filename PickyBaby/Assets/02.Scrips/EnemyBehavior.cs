using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    public bool isVariable;//여러 아이템을 스폰하는가?

    public GameObject spawnItem = null;
    public GameObject[] spawnItems = new GameObject[3];//외부에서 개수 정하기
    public GameObject destroyFx = null;
    public Animator animator = null;

    private Transform target = null;
    private float enemyMoveSpeed = 1.5f;

    //Audio
    private AudioSource audio;
    public AudioClip steppedsound;
    void Start()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.steppedsound;
        this.audio.loop = false;

        InvokeRepeating("UpdateTarget", 0.0f, 0.25f);
        if (SceneManager.GetActiveScene().name != "main02") return;
        Debug.Log("isVariable = true");
        isVariable = true;///main02 일 경우에는 variable 타입으로 바꿔준다.
    }

    private void UpdateTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 6.0f);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == "Player")
            {
                //Debug.Log("Physics Enemy: Target found");
                target = cols[i].gameObject.transform;
                return;
            }

        }
        //Debug.Log("Physics Enemy: Target lost");
        target = null;
    }

    void Update()
    {
        
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * -enemyMoveSpeed * Time.deltaTime);
            Quaternion q = Quaternion.LookRotation(dir.normalized);
            float y = q.eulerAngles.y;
            transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));

            if (!animator.GetBool("isChasing")) animator.SetBool("isChasing", true);
        }
        else
        {
            if (animator.GetBool("isChasing")) animator.SetBool("isChasing", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // Debug.Log("EnemyDie");
            GameObject fx =Instantiate(destroyFx, this.transform.position, this.transform.rotation);
            this.audio.Play();
            Destroy(fx, 1.0f);

            if (isVariable) spawnItemMultiple();
            else spawnItemSingle();
            
            Destroy(this.gameObject,0.2f);
        }
    }

    private void spawnItemSingle()//50%확률로 단일
    {
        if (Random.Range(0, 2) == 1)
        {
            GameObject item = Instantiate(spawnItem,
              new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), this.transform.rotation);
        }
    }

    private void spawnItemMultiple()
    {
        
        int rnd = (Random.Range(0, 4));
        if (spawnItems[rnd] == null)
        {
            Debug.Log("spawn vairable Null");
            return;
        }
        Debug.Log("spawn vairable "+rnd);
        GameObject item = Instantiate(spawnItems[rnd],
              new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), this.transform.rotation);
        Destroy(item, 10.0f);

    }
}
