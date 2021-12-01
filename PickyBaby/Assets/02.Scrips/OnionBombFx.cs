using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionBombFx : MonoBehaviour
{
    public GameObject bombFx;//소멸될때 효과
    public float bombsecond = 1.0f;

    public IEnumerator activeBombFx()
    {
        
        yield return new WaitForSeconds(bombsecond);
        GameObject fx = Instantiate(bombFx, transform.position, transform.rotation);
        
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
        Destroy(fx, 5.0f);
        Destroy(this.gameObject, 1.0f);
        yield return null;
    }
  

    private void OnCollisionEnter(Collision collision)
    {
        
        
        if (collision.collider.transform.CompareTag("Floor")|| collision.collider.transform.CompareTag("Enemy"))
        {
            Debug.Log("acttiveBomb");
            
            StartCoroutine(activeBombFx());
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("bombEndPos"))
            Destroy(transform.GetComponent<FlyingObstacle>());
    }
}
