using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberController : MonoBehaviour
{
    public GameObject flowerMonster;

    private void OnTriggerEnter(Collider other)
    {
        if (flowerMonster.GetComponent<StaticEnemyBehavior>().count == 2)
        {
            return;
        }
       isMatchwithTag(other.gameObject);
    }


    // collider에 들어온 물체의 tag를 검사한다.
    private void isMatchwithTag(GameObject obj)
    {
        if (obj.transform.parent.tag ==this.transform.tag)
        {
            //만약 부딫친 애가 나랑 맞는 태그를 갖고있다면?
            Debug.Log("충돌");
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.GetComponent<Rigidbody>().useGravity = false;
            //obj.transform.parent.position
            //    = new Vector3(this.transform.position.x, this.transform.position.y+1.0f, this.transform.position.z);//내 자신으로 위치고정.
            //obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = new Vector3(3, 3, 3);
            Destroy(obj.transform.parent.gameObject, 3.0f);
            flowerMonster.GetComponent<StaticEnemyBehavior>().count ++;
        }
    }
}
