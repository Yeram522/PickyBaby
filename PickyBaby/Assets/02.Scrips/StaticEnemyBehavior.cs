using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyBehavior : MonoBehaviour
{
    public GameObject endPose;// ������ ��ġ���� �����ִ� �θ� ������Ʈ
    public GameObject obstacle;
    public bool isStop;
    public int count;

    [SerializeField]
    GameObject[] endPoses = null;// ���� ������ ��ġ��

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

    IEnumerator shootOnion()//Onion ��ġ������ �������� �����ϰ� �߻��Ѵ�.
    {
        int rnd = Random.Range(0, endPose.transform.childCount);
        GameObject onion = Instantiate(obstacle,
            new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z), transform.rotation);
        onion.transform.SetParent(this.transform);
        onion.GetComponent<FlyingObstacle>().setObstacleInfo(endPoses[rnd].transform);

        StartCoroutine(shootDelay());

        yield return null ;
    }

    IEnumerator shootDelay() //���� �ð����� ������ �ȴ�.
    {
        float rnd = Random.Range(1.0f, 5.0f);
        yield return new WaitForSeconds(rnd);
        StartCoroutine(shootOnion());
        yield return null;
    }

}

