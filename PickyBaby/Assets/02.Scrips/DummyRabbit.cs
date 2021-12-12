using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRabbit : MonoBehaviour
{
    GameObject boss = null;
    public GameObject destroyFx = null;
    void Start()
    {
        boss = GameObject.Find("BossZone").transform.GetChild(0).gameObject;
        if (boss == null) return;
        if (!boss.activeSelf)
            boss = null;

        Destroy(gameObject, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //플레이어와 충돌 할때만 처리
        if (!other.CompareTag("Player")) return;
        GameObject fx = Instantiate(destroyFx, this.transform.position, this.transform.rotation);
        Destroy(fx, 1.0f);
        Destroy(this.gameObject, 0.2f);
        if (boss == null) return;//보스가 생성되어 있어야 충돌됨.
        //Debug.Log("DummyRabit<->Player");
        boss.GetComponent<BossBehavior>().Hp -= 0.1f;
       

    }
}
