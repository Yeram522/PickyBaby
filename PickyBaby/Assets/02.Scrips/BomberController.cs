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


    // collider�� ���� ��ü�� tag�� �˻��Ѵ�.
    private void isMatchwithTag(GameObject obj)
    {
        if (obj.transform.parent.tag ==this.transform.tag)
        {
            //���� �΋Hģ �ְ� ���� �´� �±׸� �����ִٸ�?
            Debug.Log("�浹");
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.GetComponent<Rigidbody>().useGravity = false;
            //obj.transform.parent.position
            //    = new Vector3(this.transform.position.x, this.transform.position.y+1.0f, this.transform.position.z);//�� �ڽ����� ��ġ����.
            //obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = new Vector3(3, 3, 3);
            Destroy(obj.transform.parent.gameObject, 3.0f);
            flowerMonster.GetComponent<StaticEnemyBehavior>().count ++;
        }
    }
}
