using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DonDestroyObject : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    public float PlayerHp;
    private void Awake() { 
        var obj = FindObjectsOfType<DonDestroyObject>(); 
        if (obj.Length == 1) { DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); } 
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "start" || SceneManager.GetActiveScene().name == "win" || SceneManager.GetActiveScene().name == "fail")
        { Destroy(gameObject); return; }

        var player = FindObjectOfType<Player>();
        Player = player.gameObject;

        if (SceneManager.GetActiveScene().name == "main01")
        {
            PlayerHp = Player.GetComponent<Player>().HP;

        }

    }

}
