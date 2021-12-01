using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyBehavior : MonoBehaviour
{
    public GameObject endPose;// 도달할 위치들을 갖고있는 부모 오브젝트
    public GameObject obstacle;

    [SerializeField]
    GameObject[] endPoses = null;// 도달 가능한 위치들

    private void Start()
    {
        //Debug.Log(endPose.transform.childCount);
        endPoses = new GameObject[endPose.transform.childCount];
        for (int i = 0; i < endPose.transform.childCount; i++)
            endPoses[i] = endPose.transform.GetChild(i).gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(shootOnion());
        }
    }

    IEnumerator shootOnion()//Onion 위치정보를 랜덤으로 생성하고 발사한다.
    {
        int rnd = Random.Range(0, endPose.transform.childCount);
        GameObject onion = Instantiate(obstacle,
            new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z), transform.rotation);
        onion.transform.SetParent(this.transform);
        onion.GetComponent<FlyingObstacle>().setObstacleInfo(endPoses[rnd].transform);
       // onion.GetComponent<FlyingObstacle>().canShoot =true;
        //yield return shootDelay();
        yield return null ;
    }

    IEnumerator shootDelay() //랜덤 시간으로 딜레이 된다.
    {
        yield return new WaitForSeconds(3.0f);
        yield return shootOnion();
    }

    IEnumerator shootStop() //특정조건 만족 시 슈팅을 멈춘다.
    {
        yield return null;
    }
}

