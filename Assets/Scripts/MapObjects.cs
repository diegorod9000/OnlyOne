using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjects : MonoBehaviour
{
    public GameObject log; 
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(log, new Vector3(2.0f, 0, 0), Quaternion.identity);
        Instantiate(log, new Vector3(0, 23.0f, 0), Quaternion.identity);
        Instantiate(log, new Vector3(0, -23.0f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
