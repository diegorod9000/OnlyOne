using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMovement : MonoBehaviour
{
    public GameObject otherFog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left*0.02f;
        if(transform.position.x < -45){
            transform.position = otherFog.transform.position + Vector3.right*86.4f;
        }
    } 
}
 