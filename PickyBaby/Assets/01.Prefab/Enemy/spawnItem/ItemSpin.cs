using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{
    private float speed = 0.001f;
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, transform.position.y + speed * Time.deltaTime, 0));
        
    }
}
