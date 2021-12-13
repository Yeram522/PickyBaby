using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyBehavior : MonoBehaviour
{
    public GameObject endPose;// 도달할 위치들을 갖고있는 부모 오브젝트
    public GameObject obstacle;
    public bool isStop;
    public int count;

    [SerializeField]
    GameObject[] endPoses = null;// 도달 가능한 위치들

    private void Start()
    {
        count = 0;
        //Debug.Log(endPose.transform.childCount);
        endPoses = new GameObject[endPose.transform.childCount];
        for (int i = 0; i < endPose.transform.childCount; i++)
            endPoses[i] = endPose.transform.GetChild(i).gameObject;
        isStop = false;
        StartCoroutine(shootOnion());
    }

    private void Update()
    {
        if (count == 2)
            StopAllCoroutines();
    }

    IEnumerator shootOnion()//Onion 위치정보를 랜덤으로 생성하고 발사한다.
    {
        int rnd = Random.Range(0, endPose.transform.childCount);
        GameObject onion = Instantiate(obstacle,
            new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z), transform.rotation);
        onion.transform.SetParent(this.transform);
        onion.GetComponent<FlyingObstacle>().setObstacleInfo(endPoses[rnd].transform);

        StartCoroutine(shootDelay());

        yield return null ;
    }

    IEnumerator shootDelay() //랜덤 시간으로 딜레이 된다.
    {
        float rnd = Random.Range(1.0f, 5.0f);
        yield return new WaitForSeconds(rnd);
        StartCoroutine(shootOnion());
        yield return null;
    }

}

