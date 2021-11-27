using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObstacle : MonoBehaviour
{
    [SerializeField]
    private float flytime = 3.0f; //���ư��� �ð�, ���� �������� ������ ����.
    [SerializeField]
    private float reduceHeight = 0.5f;//�������� ����

    private float startTime;
    private float duringTime;
    public Transform startpos = null;
    public Transform endpos = null;
    public bool canShoot;

    public void setObstacleInfo(Transform _endpos)//����������Ʈ�� ��ź�� ������ġ������ �������ش�.
    {
        Debug.Log("setObstacleInfo");
        startpos = this.transform;
        endpos = _endpos;
        
        StartCoroutine(DelayUpdate());
    }
  


    private void Update()
    {
        //if (!canShoot) return;
          
        //if (transform.position == endpos.position)
        //{
        //    Debug.Log("�浹");
        //    Destroy(this);
        //}
        //Vector3 center = (startpos.position + endpos.position) * 0.5f;

        //center -= new Vector3(0, 1.0f * reduceHeight, 0);//y���� ���̸� ���̰� ��������.
        //Vector3 riseRelCenter = startpos.position - center;
        //Vector3 setRelCenter = endpos.position - center;
        //float fracComplete = (Time.time - startTime) / flytime;
        //transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        //transform.position += center;
        
    }

    IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(0.2f);
        startTime = Time.time;
        canShoot = true;
        yield return StartCoroutine(Flying());
    }

    IEnumerator Flying()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.001f);
            if (transform.position == endpos.position)
            {
                Debug.Log("�浹");
                Destroy(this);
                yield return null;
            }
            Vector3 center = (startpos.position + endpos.position) * 0.5f;

            center -= new Vector3(0, 1.0f * reduceHeight, 0);//y���� ���̸� ���̰� ��������.
            Vector3 riseRelCenter = startpos.position - center;
            Vector3 setRelCenter = endpos.position - center;
            float fracComplete = (Time.time - startTime) / flytime;
            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;
        }

        yield return null;
    }


}
