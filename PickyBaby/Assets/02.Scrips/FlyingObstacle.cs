using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObstacle : MonoBehaviour
{
    

    [SerializeField]
    private float flytime = 150.0f; //���ư��� �ð�, ���� �������� ������ ����.
    [SerializeField]
    private float reduceHeight = 0.5f;//�������� ����

    private OnionBombFx bombscript;
    private float startTime;
    public Transform startpos = null;
    public Transform endpos = null;

    public void setObstacleInfo(Transform _startpos,Transform _endpos)//����������Ʈ�� ��ź�� ������ġ������ �������ش�.
    {
        startpos = _startpos;
        endpos = _endpos;
    }

    void Start()
    {
        bombscript = this.transform.GetComponent<OnionBombFx>();
        startTime = Time.time;
    }

    
    void Update()
    {
        if (transform.position == endpos.position)
        {
            
            Destroy(this);

        }
        Vector3 center = (startpos.position + endpos.position) * 0.5f;

        center -= new Vector3(0, 1.0f * reduceHeight, 0);//y���� ���̸� ���̰� ��������.
        Vector3 riseRelCenter = startpos.position - center;
        Vector3 setRelCenter = endpos.position - center;
        float fracComplete = (Time.time - startTime) / flytime;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }

    IEnumerator Fly2Endpos()
    {
        startTime = Time.time;
        while (true)
        {
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
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.collider.transform.CompareTag("Floor"))
        //{
        //    Debug.Log("���� �浹");
        //    Destroy(this.gameObject, 1.0f);

        //    Destroy(this);
        //}
    }
  
}
