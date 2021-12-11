using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollideChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;
    // Start is called before the first frame update

    private void Start()
    {
        parent = transform.parent.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            Debug.Log("당근 맞ㅇ므");
            StartCoroutine(damage());
            
        }
    }

    private IEnumerator damage()
    {
        yield return new WaitForSeconds(2.0f);
        parent.GetComponent<BossBehavior>().Hp -= 0.1f;
        yield return null;
    }
}
