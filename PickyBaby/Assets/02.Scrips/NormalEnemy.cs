using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    public Getshield getshield;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") && !collision.gameObject.CompareTag("shield"))
        {
            Debug.Log("�÷��̾�� ������ �浹");
            if (getshield.Shield > 0)
            {
                return;
            }
                  

            collision.gameObject.GetComponent<Player>().HP -= 0.05f;
        }
    }
}
