using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrotBomb : MonoBehaviour
{
    //Logic
    //1. �����Ǹ� Ư�� �ð� �� ������.
    //2. ���� ���� ����
    // -  �ٴڿ� tag"Floor"�϶� isbombspread = true
    // -  �������� �¾�����, player ���� Ȥ�� �䳢�� �¾��� ���� ȥ�� ����. 

    public GameObject bombFX;
    public GameObject groundFX;
    public GameObject power;
    public Material submaterial;//����� Ż ��? ���͸���.
    private bool _isexpend = true;
    private bool istrigger;
    void Start()
    {
        istrigger = false;
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
    IEnumerator castingbomb()//�Ϲ� 4�ʵ� ����
    {
        yield return new WaitForSeconds(3.0f);//3�� ��
        this.transform.GetComponent<Renderer>().material = submaterial;//������ ���͸���� �ٲ��?
        yield return new WaitForSeconds(1.0f);//1�� ��

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
        if (collision.collider.transform.CompareTag("Player")) _isexpend = false;
        if (collision.collider.transform.CompareTag("Floor")) _isexpend = true;

        if (!istrigger)//�Ʒ��� �ѹ��� ����
        {
            StartCoroutine(castingbomb());//� �Ͱ� �浹���ϸ� 4��(������ ���)
            //player�� ���� ������ expended ���� ����
            istrigger = true;
        }
    }
}