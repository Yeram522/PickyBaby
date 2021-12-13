using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
    public GameObject bossUI;//HP UI
    public GameObject zone;
    public GameObject jbearPfb;//젤리곰 프리팹
    public GameObject firePfb;//불지르기 프리팹
    public GameObject carrotPfb;//당근폭탄 프리팹

    private Slider uiHp;
    [SerializeField]
    public float Hp;//Boss HP
    [SerializeField]
    private GameObject[] attackzone = null;
    [SerializeField]
    private Animator animator = null;
    private void Start()
    {
        bossUI.transform.parent.gameObject.SetActive(true);//보스 체력 UI 활성화

        animator = this.GetComponent<Animator>();
        uiHp = bossUI.GetComponent<Slider>();
        Hp = 1.0f;
        attackzone = new GameObject[zone.transform.childCount];
        //스킬을 뿌릴 지점을 배열에 넣는다.
        for (int i = 0; i < zone.transform.childCount; i++)
        {
            attackzone[i] = zone.transform.GetChild(i).gameObject;
        }

        StartCoroutine(isAttacable());
    }

    private void Update()
    {
        uiHp.value = Hp;

        if (Hp <= 0) SceneManager.LoadScene("fail");

        this.transform.localPosition = new Vector3(0,0,0);
        this.transform.localRotation = Quaternion.Euler(new Vector3(0, -110, 0));
     
    }

     IEnumerator isAttacable()//공격가능한 상태
    {
        while(Hp>0)//Boss의 HP가 0보다 크면 계속 공격이 가능하다.
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 7.0f));//랜덤으로 스킬간의 간격 설정
            StartCoroutine(SelectAttackVar());//랜덤으로 스킬 뿌림.
        }
    }

    private int selectRandomPlace()//스킬이 발생되지 않은 지점만 고른다.
    {
        while(true)
        {
            int n = Random.Range(0, zone.transform.childCount);
            if(attackzone[n].transform.childCount==0)//스킬이 없는 지점을 골라서 인덱스를 리턴. 있다면 재시도.         
                return n;
            
        }
              
    }

     IEnumerator SelectAttackVar()
    {
        int n = Random.Range(0, 3);//0~3까지
        //int n = 2;//0~3까지
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
    IEnumerator Attack01()//몹 생성 패턴//여러마리를 드롭한다.
    {
        animator.SetTrigger("attack1");
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Boss: 몹을 생성합니다");
        int count = Random.Range(3, 6); //최소 3마리. 최대 6마리까지
        for(int i = 0; i<count;i++)
        {
            Transform place = attackzone[selectRandomPlace()].transform;
            GameObject mop = Instantiate(jbearPfb, place.position, place.rotation);
            mop.transform.SetParent(place);
        }    
        yield return null;
    }

    IEnumerator Attack02()//불 지르기
    {
        animator.SetTrigger("attack2");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Boss: 불을 지릅니다");
        int count = Random.Range(5, 10); //최소 3마리. 최대 6마리까지
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

    IEnumerator Attack03()//당근폭탄 던지기
    {
        animator.SetTrigger("attack3");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Boss: 당근폭탄을 던집니다.");

        int count = Random.Range(5, 10); //최소 3마리. 최대 6마리까지
        for (int i = 0; i < count; i++)
        {
            Transform place = attackzone[selectRandomPlace()].transform;
            Vector3 pos = new Vector3(place.position.x, place.position.y + 30.0f, place.position.z);//약간 위에서 떨어트리기!
            GameObject carrot = Instantiate(carrotPfb, place.position, carrotPfb.transform.rotation);
            carrot.transform.SetParent(place);
        }
       
        //carrot.transform.localScale = new Vector3(1, 1, 1);
       // carrot.transform.localPosition = new Vector3(0, 0, 0);
        yield return null;
    }

}
