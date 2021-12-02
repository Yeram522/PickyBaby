using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrotBomb : MonoBehaviour
{
    //Logic
    //1. �����Ǹ� Ư�� �ð� �� ������.
    //2. ���� ���� ����
    // -  �ٴڿ� tag"Floor"�϶� isbombspread = true
    // -  �������� �¾�����, player ���� Ȥ�� �䳢�� �¾��� ���� ȥ�� ����. isbombspread = false

    public GameObject bombFX;
    public GameObject groundFX;
    public GameObject power;
    public Material submaterial;//����� Ż ��? ���͸���.
    private bool isTrigger;
    void Start()
    {
        isTrigger = false;


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(castingbomb(false));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(castingbomb(true));
        }
    }

    IEnumerator expendedBomb()//������� ���� ������ ���
    {
       
        power.SetActive(true);
        GameObject bmbFx = Instantiate(bombFX, this.transform.position, this.transform.rotation);
        GameObject[] powers = new GameObject[power.transform.childCount];
        for(int i = 0; i< power.transform.childCount;i++)
        {
            powers[i] = power.transform.GetChild(i).gameObject;
            GameObject Fx = Instantiate(groundFX, powers[i].transform.position, powers[i].transform.rotation);
            //Fx.transform.SetParent(powers[i].transform);
            //Fx.transform.localScale = new Vector3(1, 1, 1);

            Destroy(this.transform.parent.gameObject, 1.0f);
            Destroy(Fx, 15.0f);
        }
        Destroy(bmbFx, 3.0f);
        yield return null;
    }
    IEnumerator castingbomb(bool _isexpend)//�Ϲ� 4�ʵ� ����
    {
        yield return new WaitForSeconds(3.0f);//4�� ��
        this.transform.GetComponent<Renderer>().material = submaterial;//������ ���͸���� �ٲ��?
        yield return new WaitForSeconds(1.0f);//4�� ��

        if(_isexpend) StartCoroutine(expendedBomb());
        else StartCoroutine(bomb());

        yield return null;
    }

    IEnumerator bomb()//�ٷ� ����
    {       
        GameObject Fx = Instantiate(bombFX, this.transform.position, this.transform.rotation);
        
       /*Fx.transform.SetParent(this.transform);
        Fx.transform.localScale = new Vector3(1, 1, 1);*/
        Destroy(this.transform.parent.gameObject, 0.6f);
        Destroy(Fx, 3.0f);
       yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTrigger) return;//ù��°�� �浹���� �����ϰ� �� �����浹�� ó�� ���� �ʴ´�.
        if (collision.collider.CompareTag("Floor"))
        {
            isTrigger = true;
            StartCoroutine(castingbomb(true));
            return;
        }
        else if (collision.collider.CompareTag("Boss"))
        {
            isTrigger = true;
            //�ٷ�������.

            StartCoroutine(bomb());
        }
        else
        {
            isTrigger = true;
            StartCoroutine(castingbomb(false));
        }
    }
}
