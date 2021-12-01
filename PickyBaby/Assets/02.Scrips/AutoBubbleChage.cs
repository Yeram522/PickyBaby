using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBubbleChage : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject text1;
    [SerializeField]
    private GameObject text2;
    // Start is called before the first frame update
    void Start()
    {
        panel = this.transform.Find("Canvas").GetChild(0).gameObject;
        text1 = panel.transform.GetChild(0).gameObject;
        text2 = panel.transform.GetChild(1).gameObject;
        StartCoroutine(chageBubble());
    }

    IEnumerator chageBubble()
    {
        while(true)
        {
            text1.SetActive(true);
            text2.SetActive(false);
            yield return new WaitForSeconds(5.0f);
            text1.SetActive(false);
            text2.SetActive(true);
            yield return new WaitForSeconds(5.0f);
        }

        yield return null;
    }
}
