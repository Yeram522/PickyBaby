using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� ������ �浹");
            collision.gameObject.GetComponent<Player>().HP -= 0.05f;
        }
    }
}
