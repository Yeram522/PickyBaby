using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goTitle()
    {
        SceneManager.LoadScene("start");
    }

    public void endGame()
    {
        Application.Quit();
    }

    public void Gomain1()
    {
        SceneManager.LoadScene("main01");
    }

    public void Gomain2()
    {
        SceneManager.LoadScene("main02");
    }
}
