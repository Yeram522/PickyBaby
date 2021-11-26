using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionBombFx : MonoBehaviour
{
    public GameObject bombFx;//소멸될때 효과

    private FlyingObstacle obstaclescript;

    public void activebomb()
    {
        StartCoroutine(activeBombFx());
    }

    public IEnumerator activeBombFx()
    {
        
        yield return new WaitForSeconds(2.0f);
        GameObject fx = Instantiate(bombFx, transform.position, transform.rotation);
        
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
        Destroy(fx, 2.0f);
        Destroy(this.gameObject, 1.0f);
        yield return null;
    }
    void Start()
    {
        obstaclescript = this.gameObject.GetComponent<FlyingObstacle>();
        StartCoroutine(activeBombFx());
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.collider.transform.CompareTag("Floor"))
        {
            Debug.Log("양파 충돌");
            
            StartCoroutine(activeBombFx());
        }
    }
}
