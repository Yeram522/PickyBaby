using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrotBomb : MonoBehaviour
{
    //Logic
    //1. 생성되면 특정 시간 뒤 터진다.
    //2. 터질 때의 조건
    // -  바닥에 tag"Floor"일때 isbombspread = true
    // -  누구한테 맞았을때, player 소유 혹은 토끼가 맞았을 때는 혼자 폭발. isbombspread = false

    public GameObject bombFX;
    public GameObject groundFX;
    public GameObject power;
    public Material submaterial;//당근이 탈 때? 머터리얼.
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

    IEnumerator expendedBomb()//사방으로 같이 터지는 경우
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
    IEnumerator castingbomb(bool _isexpend)//일반 4초뒤 터짐
    {
        yield return new WaitForSeconds(3.0f);//4초 뒤
        this.transform.GetComponent<Renderer>().material = submaterial;//검정색 머터리얼로 바뀌고?
        yield return new WaitForSeconds(1.0f);//4초 뒤

        if(_isexpend) StartCoroutine(expendedBomb());
        else StartCoroutine(bomb());

        yield return null;
    }

    IEnumerator bomb()//바로 터짐
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
        if (isTrigger) return;//첫번째의 충돌에만 반응하고 그 이후충돌은 처리 하지 않는다.
        if (collision.collider.CompareTag("Floor"))
        {
            isTrigger = true;
            StartCoroutine(castingbomb(true));
            return;
        }
        else if (collision.collider.CompareTag("Boss"))
        {
            isTrigger = true;
            //바로터진다.

            StartCoroutine(bomb());
        }
        else
        {
            isTrigger = true;
            StartCoroutine(castingbomb(false));
        }
    }
}
