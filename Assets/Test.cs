using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Collectables/2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
