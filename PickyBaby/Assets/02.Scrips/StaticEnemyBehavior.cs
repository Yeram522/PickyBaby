using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyBehavior : MonoBehaviour
{
    public GameObject endPose;// 도달할 위치들을 갖고있는 부모 오브젝트
    public GameObject obstacle;

    [SerializeField]
    GameObject[] endPoses = null;// 도달 가능한 위치들

    private void Start()
    {
        for (int i = 0; i < endPose.transform.childCount; i++)
            endPoses[i] = endPose.transform.GetChild(i).gameObject;
    }

    private void Update()
    {
        
    }
}
