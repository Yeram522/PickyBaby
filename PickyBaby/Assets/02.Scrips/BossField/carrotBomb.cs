using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrotBomb : MonoBehaviour
{
    //Logic
    //1. 생성되면 특정 시간 뒤 터진다.
    //2. 터질 때의 조건
    // -  바닥에 tag"Floor"일때 isbombspread = true
    // -  누구한테 맞았을때, player 소유 혹은 토끼가 맞았을 때는 혼자 폭발. 

    public GameObject bombFX;
    public GameObject groundFX;
    public GameObject power;
    public Material submaterial;//당근이 탈 때? 머터리얼.
    private bool _isexpend = true;
    private bool istrigger;
    private GameObject collisionObj;
    void Start()
    {
        istrigger = false;
        Destroy(this.transform.parent.gameObject, 10.0f);//어떤 이유로 충돌받지 않으면 자동으로 소멸
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
            Fx.transform.SetParent(powers[i].transform);
            //Fx.transform.localScale = new Vector3(1, 1, 1);

            Destroy(this.transform.gameObject, 1.0f);
            Destroy(Fx, 15.0f);
        }
        Destroy(bmbFx, 3.0f);
        yield return null;
    }
    IEnumerator castingbomb()//일반 4초뒤 터짐
    {
        yield return new WaitForSeconds(4.0f);//3초 뒤
        this.transform.GetComponent<Renderer>().material = submaterial;//검정색 머터리얼로 바뀌고?
        yield return new WaitForSeconds(2.0f);//1초 뒤

        if(_isexpend) StartCoroutine(expendedBomb());
        else StartCoroutine(bomb());

        yield return null;
    }

    IEnumerator bomb()//바로 터짐
    {       
        GameObject Fx = Instantiate(bombFX, this.transform.position, this.transform.rotation);

        if (!_isexpend && collisionObj.CompareTag("Player"))
            collisionObj.GetComponent<Player>().HP -= 0.2f;
        Destroy(this.transform.parent.gameObject, 0.6f);
        Destroy(Fx, 3.0f);
       yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {

        collisionObj = collision.gameObject;
        if (collision.collider.transform.CompareTag("Player"))
            _isexpend = false;
        
        if (collision.collider.transform.CompareTag("Floor")) _isexpend = true;

        if (!istrigger)//아래는 한번만 실행
        {
            StartCoroutine(castingbomb());//어떤 것과 충돌을하면 4초(터지기 대기)
            //player가 줍지 않으면 expended 모드로 실행
            istrigger = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.transform.CompareTag("Player"))
        {
            _isexpend = true;
            collisionObj = null;
        }
    }
}
