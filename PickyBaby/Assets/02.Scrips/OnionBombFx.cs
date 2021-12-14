using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionBombFx : MonoBehaviour
{
    public GameObject bombFx;//소멸될때 효과
    public float bombsecond = 1.0f;

    //Audio
    private AudioSource audio;
    public AudioClip bombsound;
    private void Start()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.bombsound;
        this.audio.loop = false;
    }
    public IEnumerator activeBombFx()
    {
        
        yield return new WaitForSeconds(bombsecond);
        this.audio.Play();
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
            //Debug.Log("acttiveBomb");
            
            StartCoroutine(activeBombFx());
        }    
        
        if(collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Player>().HP -= 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("bombEndPos"))
            Destroy(transform.GetComponent<FlyingObstacle>());
    }
}
