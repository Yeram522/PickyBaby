using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyBehavior : MonoBehaviour
{
    public GameObject endPose;// ������ ��ġ���� �����ִ� �θ� ������Ʈ
    public GameObject obstacle;

    [SerializeField]
    GameObject[] endPoses = null;// ���� ������ ��ġ��

    private void Start()
    {
        for (int i = 0; i < endPose.transform.childCount; i++)
            endPoses[i] = endPose.transform.GetChild(i).gameObject;
    }

    private void Update()
    {
        
    }
}
