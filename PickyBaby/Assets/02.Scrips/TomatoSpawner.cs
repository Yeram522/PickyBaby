using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSpawner : MonoBehaviour
{
    public GameObject spawnObj;//tomato
    // Start is called before the first frame update
    public bool stopSpawn=true;
    void Start()
    {
        StartCoroutine(spawnTomato());
    }

    IEnumerator spawnTomato()
    {
        while(stopSpawn)
        {         
            GameObject obj = Instantiate(spawnObj, transform.position, transform.rotation);
            yield return new WaitForSeconds(7.0f);
        }

        yield return null;
    }
}
