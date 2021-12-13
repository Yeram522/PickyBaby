using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map2UImanager : MonoBehaviour
{
    public GameObject[] board1;
    public GameObject[] board2;

 
    // Start is called before the first frame update
    void Start()
    {
        //board1 = new GameObject[3];//Penel, text1,text2
        //board2 = new GameObject[3];

        board1[0].SetActive(true);

        StartCoroutine(ShowBoard(board1));
    }

    IEnumerator ShowBoard(GameObject[] obj)
    {
        while(true)
        {
            Color color = obj[0].GetComponent<Image>().color;
            if (color.a >= 1.0f) break;
            color.a += 0.1f;
            obj[0].GetComponent<Image>().color = color;

            for(int i = 1; i< obj.Length;i++)
            {
                Color txtcolor = obj[i].GetComponent<Text>().color;
                if (txtcolor.a == 1.0f) break;
                txtcolor.a += 0.1f;
                obj[i].GetComponent<Text>().color = txtcolor;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(DeleteBoard(obj));
        yield return null;
    }

    IEnumerator DeleteBoard(GameObject[] obj)
    {
        Debug.Log("¡¯¿‘");
        while (true)
        {
            Color color = obj[0].GetComponent<Image>().color;
            if (color.a == 0.0f) break;
            color.a -= 0.1f;
            obj[0].GetComponent<Image>().color = color;

            for (int i = 1; i < obj.Length; i++)
            {
                Color txtcolor = obj[i].GetComponent<Text>().color;
                if (txtcolor.a <= 0.0f)
                {
                    obj[0].transform.gameObject.SetActive(false);
                    break;
                }
                txtcolor.a -= 0.1f;
                obj[i].GetComponent<Text>().color = txtcolor;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    public void showUIBoard2()
    {
        board2[0].SetActive(true);
      
        StartCoroutine(ShowBoard(board2));
        return;
    }
}
