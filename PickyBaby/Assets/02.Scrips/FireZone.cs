using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    public GameObject fireFX;
    [SerializeField]
    GameObject[] firedposes = null;//9�� �ʵ�����
                                   //material ��
    [SerializeField]
    Material[] posesmaterial = null;
    GameObject[] fire = null;
    //����Ʈ������
    void Start()
    {
        firedposes = new GameObject[this.transform.childCount];
        posesmaterial = new Material[this.transform.childCount];
        fire = new GameObject[this.transform.childCount];
        //��Ʈ��Ʈ�� ������ ������Ʈ�� �ڽ� ������Ʈ���� �迭�� �ִ´�.
        for (int i = 0; i< this.transform.childCount; i++)
        {
            firedposes[i] = this.transform.GetChild(i).gameObject;
            posesmaterial[i] = firedposes[i].GetComponent<Renderer>().material;
        }

        StartCoroutine(beforeCastingFire());
    }

    IEnumerator beforeCastingFire()//�� ������ ���� �̸� ������ ���� ��ġ�� �����ش�. ���� A�� ������.
    {
        Debug.Log("[Start Coroutine]:beforeCastingfire");
        while(true)
        {

            if (posesmaterial[0].color.a >= 0.6f)
            {
                Debug.Log("Fire!");
                StartCoroutine(setFire());
                break;
            }
            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i < this.transform.childCount; i++)
            {
                Color color = posesmaterial[i].color;
                color.a += 0.05f;
                posesmaterial[i].SetColor("_Color", color);
            }
        }
        yield return null;
    }

    IEnumerator setFire()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            fire[i] = Instantiate(fireFX, firedposes[i].transform.position, firedposes[i].transform.rotation);
            fire[i].transform.SetParent(firedposes[i].transform);//�ش� ������ �θ�� ����.
            fire[i].transform.localScale = new Vector3(1, 1, 1);

            Destroy(this.gameObject, 5.0f);
            Destroy(fire[i].gameObject, 5.5f);
        }
        yield return null;
    }

   
}
