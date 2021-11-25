using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject destroyFx;

    private Transform target = null;
    private float enemyMoveSpeed = 1.5f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0.0f, 0.25f);
    }

    private void UpdateTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 3.0f);
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

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);
            Quaternion q = Quaternion.LookRotation(dir.normalized);
            float y = q.eulerAngles.y;
            transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("EnemyDie");
            GameObject fx =Instantiate(destroyFx, this.transform.position, this.transform.rotation);
            Destroy(fx, 1.0f);
            Destroy(this.gameObject,0.2f);
        }
    }
}
