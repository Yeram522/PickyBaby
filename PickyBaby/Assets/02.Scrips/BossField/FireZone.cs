using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    public GameObject fireFX;
    [SerializeField]
    GameObject[] firedposes = null;//9개 필드정보
                                   //material 값
    [SerializeField]
    Material[] posesmaterial = null;
    GameObject[] fire = null;
    //이펙트프리펩
    void Start()
    {
        firedposes = new GameObject[this.transform.childCount];
        posesmaterial = new Material[this.transform.childCount];
        fire = new GameObject[this.transform.childCount];
        //스트립트에 부착된 오브젝트의 자식 오브젝트들을 배열에 넣는다.
        for (int i = 0; i< this.transform.childCount; i++)
        {
            firedposes[i] = this.transform.GetChild(i).gameObject;
            posesmaterial[i] = firedposes[i].GetComponent<Renderer>().material;
        }

        StartCoroutine(beforeCastingFire());
    }

    IEnumerator beforeCastingFire()//불 지르기 전에 미리 투명도로 예상 위치를 보여준다. 점점 A값 진해짐.
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
            fire[i].transform.SetParent(firedposes[i].transform);//해당 지점을 부모로 셋팅.
            fire[i].transform.localScale = new Vector3(1, 1, 1);

            Destroy(this.gameObject, 5.0f);
            Destroy(fire[i].gameObject, 5.5f);
        }
        yield return null;
    }

   
}
