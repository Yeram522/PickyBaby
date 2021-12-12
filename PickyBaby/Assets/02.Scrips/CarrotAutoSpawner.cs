using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotAutoSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject carrot;
    private bool trigger;
    private void Start()
    {
        trigger = true;
    }

    IEnumerator createCarrot()
    {
        yield return new WaitForSeconds(5.0f);
        GameObject obj = Instantiate(carrot, transform.position, transform.rotation);
        obj.transform.localScale = Vector3.one;
        obj.transform.SetParent(this.transform);

        trigger = true;
        yield return null;
    }
 

    private void Update()
    {
        if(transform.childCount == 0 && trigger)
        {
            trigger = false;
            StartCoroutine("createCarrot");
        }
    }
}
