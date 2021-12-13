using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float time = 180;
    private int currentTime;
    public bool startTimer = false;
    void Start()
    {
     //   timerText = GetComponent<Text>();
    }
    void Update()
    {
        if (startTimer == true)
        {
            time -= Time.deltaTime;
            currentTime = (int)time;
            timerText.text = "�����ð� : " + currentTime;
        }

        if (time <= 0)
        {
            SceneManager.LoadScene("fail");
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
    
        if (other.transform.tag == "Player")
        {
            startTimer = true;
        }
    }

}
