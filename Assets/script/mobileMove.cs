using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobileMove : MonoBehaviour
{
    public GameObject mobileImg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mobileImg.transform.Translate(1 * Time.deltaTime, 0, 0);
    }
}
