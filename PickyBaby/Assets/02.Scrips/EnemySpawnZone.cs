using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnZone : MonoBehaviour
{
    public GameObject spawnobj;//스폰할 게임오브젝트
    public int spawncount;//몇마리 스폰할지

    [SerializeField]
    GameObject[] zone = null;
   
   
    GameObject group = null;
   
    void Start()
    {
        group = transform.GetChild(0).gameObject;
        updateSpawnZoneInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            selectRndZone2Spawn();
        }
    }

    void updateSpawnZoneInfo()
    {
        zone = new GameObject[group.transform.childCount];
        for (int i = 0; i < group.transform.childCount; i++)       
            zone[i] = group.transform.GetChild(i).gameObject;
    }

    void selectRndZone2Spawn()
    {
        List<int> rndzone = new List<int>();

        for (int i = 0; i < spawncount;)
        {
            int rnd = Random.Range(0, group.transform.childCount);

            if (rndzone.Contains(rnd))
            {
                continue;
            }
            else
            {
                rndzone.Add(rnd);
                //spawn
                spawnEnemy(rnd);
                i++;
            }
        }
    }

    void spawnEnemy(int _rnd)
    {
        GameObject enemy = Instantiate(spawnobj, zone[_rnd].transform.position, spawnobj.transform.rotation);
    }
}
