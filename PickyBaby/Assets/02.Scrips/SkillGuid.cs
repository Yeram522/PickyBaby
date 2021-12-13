using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGuid : MonoBehaviour
{
    public GameObject txt;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) return;
        Player obj = collision.gameObject.GetComponent<Player>();
        if (obj.doublejump || obj.lifeBack || obj.isShield)
        {
            Destroy(gameObject);
            return;
        }
        txt.SetActive(true);
        StartCoroutine(delayshowing());
    }

    IEnumerator delayshowing()
    {
        yield return new WaitForSeconds(3.0f);
        txt.SetActive(false);
    }
}
