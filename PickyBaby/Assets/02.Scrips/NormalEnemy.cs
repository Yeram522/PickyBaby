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
            Debug.Log("플레이어와 곰돌이 충돌");
            if(collision.collider.GetComponent<Player>().isShield)
            {
                getshield = collision.collider.transform.GetChild(4).gameObject.GetComponent<Getshield>();
                
                if (getshield.Shield > 0)
                {
                    Debug.Log(getshield.Shield);
                    return;
                }
            }    
             
            collision.gameObject.GetComponent<Player>().HP -= 0.05f;
        }
    }
}
