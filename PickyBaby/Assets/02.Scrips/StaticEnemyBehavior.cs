using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyBehavior : MonoBehaviour
{
    public GameObject endPose;// ������ ��ġ���� �����ִ� �θ� ������Ʈ
    public GameObject obstacle;

    [SerializeField]
    GameObject[] endPoses = null;// ���� ������ ��ġ��

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

    IEnumerator shootOnion()//Onion ��ġ������ �������� �����ϰ� �߻��Ѵ�.
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

    IEnumerator shootDelay() //���� �ð����� ������ �ȴ�.
    {
        yield return new WaitForSeconds(3.0f);
        yield return shootOnion();
    }

    IEnumerator shootStop() //Ư������ ���� �� ������ �����.
    {
        yield return null;
    }
}

