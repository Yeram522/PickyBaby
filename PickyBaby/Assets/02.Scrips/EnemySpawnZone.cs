using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnZone : MonoBehaviour
{
    public GameObject spawnobj;//스폰할 게임오브젝트
    public GameObject spawnfx; //스폰할 때 나오는 효과
    public int spawncount;//몇마리 스폰할지

    [SerializeField]
    GameObject[] zone = null;

    List<int> rndzone = new List<int>();
    GameObject group = null;
   
    void Start()
    {
        group = transform.GetChild(0).gameObject;
        updateSpawnZoneInfo();
        selectRndZone2Spawn();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            selectRndZone2Spawn();
        }

        if(isEmpty())
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
        rndzone = new List<int>();

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
                StartCoroutine(spawnEnemy(rnd));
                i++;
            }
        }
    }

    IEnumerator spawnEnemy(int _rnd)
    {
        Vector3 fxpos = new Vector3(zone[_rnd].transform.position.x, zone[_rnd].transform.position.y+0.5f, zone[_rnd].transform.position.z);
        GameObject fx = Instantiate(spawnfx, fxpos, spawnfx.transform.rotation);
        fx.transform.SetParent(zone[_rnd].transform);
        Destroy(fx, 1.5f);
        //Spawn effect
        yield return new WaitForSeconds(1.0f);
        GameObject enemy = Instantiate(spawnobj, zone[_rnd].transform.position, spawnobj.transform.rotation);
        enemy.transform.SetParent(zone[_rnd].transform);
        yield return null;
    }

    bool isEmpty() //생성된 모든 enemy를 제거하면 true반환
    {
        for(int i = 0; i< spawncount;i++)
        {
            if (zone[rndzone[i]].transform.childCount != 0) return false;
        
        }
        return true;
    }
}
