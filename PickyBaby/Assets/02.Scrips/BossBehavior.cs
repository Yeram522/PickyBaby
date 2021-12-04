using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public GameObject zone;
    public GameObject jbearPfb;//������ ������
    public GameObject firePfb;//�������� ������
    public GameObject carrotPfb;//�����ź ������
   
    [SerializeField]
    private float Hp;//Boss HP
    [SerializeField]
    private GameObject[] attackzone = null;
    [SerializeField]
    private Animator animator = null;
    private void Start()
    {
        animator = this.GetComponent<Animator>();
        Hp = 100.0f;
        attackzone = new GameObject[zone.transform.childCount];
        //��ų�� �Ѹ� ������ �迭�� �ִ´�.
        for (int i = 0; i < zone.transform.childCount; i++)
        {
            attackzone[i] = zone.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        this.transform.localPosition = new Vector3(0,0,0);
        this.transform.localRotation = Quaternion.Euler(new Vector3(0, -110, 0));
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(isAttacable());
        }
     
    }

    IEnumerator isAttacable()//���ݰ����� ����
    {
        while(Hp>0)//Boss�� HP�� 0���� ũ�� ��� ������ �����ϴ�.
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 7.0f));//�������� ��ų���� ���� ����
            StartCoroutine(SelectAttackVar());//�������� ��ų �Ѹ�.
        }
    }

    private int selectRandomPlace()//��ų�� �߻����� ���� ������ ����.
    {
        while(true)
        {
            int n = Random.Range(0, zone.transform.childCount);
            if(attackzone[n].transform.childCount==0)//��ų�� ���� ������ ��� �ε����� ����. �ִٸ� ��õ�.         
                return n;
            
        }
              
    }

    IEnumerator SelectAttackVar()
    {
        
        int n = Random.Range(0, 3);//0~2����
        switch (n)
        {
            case 0:
                StartCoroutine(Attack01());
                break;
            case 1:
                StartCoroutine(Attack02());
                break;
            case 2:
                StartCoroutine(Attack03());
                break;
        }
        yield return null;
    }
    IEnumerator Attack01()//�� ���� ����//���������� ����Ѵ�.
    {
        animator.SetTrigger("attack1");
        yield return new WaitForSeconds(0.7f);
        Debug.Log("Boss: ���� �����մϴ�");
        int count = Random.Range(3, 6); //�ּ� 3����. �ִ� 6��������
        for(int i = 0; i<count;i++)
        {
            Transform place = attackzone[selectRandomPlace()].transform;
            GameObject mop = Instantiate(jbearPfb, place.position, place.rotation);
            mop.transform.SetParent(place);
        }    
        yield return null;
    }

    IEnumerator Attack02()//�� ������
    {
        animator.SetTrigger("attack2");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Boss: ���� �����ϴ�");
        int count = Random.Range(5, 10); //�ּ� 3����. �ִ� 6��������
        for (int i = 0; i < count; i++)
        {
            Transform place = attackzone[selectRandomPlace()].transform;
            GameObject fire = Instantiate(firePfb, place.position, firePfb.transform.rotation);
            fire.transform.SetParent(place);
            fire.transform.localRotation = Quaternion.Euler(new Vector3(0, 5, 0));
            fire.transform.localScale = new Vector3(1, 1, 1);
            fire.transform.localPosition = new Vector3(20.0f, -15.2f, 6.5f);
        }
        

        yield return null;
    }

    IEnumerator Attack03()//�����ź ������
    {
        animator.SetTrigger("attack3");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Boss: �����ź�� �����ϴ�.");

        int count = Random.Range(5, 10); //�ּ� 3����. �ִ� 6��������
        for (int i = 0; i < count; i++)
        {
            Transform place = attackzone[selectRandomPlace()].transform;
            Vector3 pos = new Vector3(place.position.x, place.position.y + 30.0f, place.position.z);//�ణ ������ ����Ʈ����!
            GameObject carrot = Instantiate(carrotPfb, place.position, carrotPfb.transform.rotation);
            carrot.transform.SetParent(place);
        }
       
        //carrot.transform.localScale = new Vector3(1, 1, 1);
       // carrot.transform.localPosition = new Vector3(0, 0, 0);
        yield return null;
    }
}
