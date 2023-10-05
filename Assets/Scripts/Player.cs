using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour 
{
    // Start is called before the first frame update
    public KeyCode upKey, downKey, leftKey, rightKey;
    
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+= Vector3.left*0.02f;
        if (Input.GetKey(upKey)){
            transform.position+= Vector3.up*0.035f;
        }
        if (Input.GetKey(downKey)){
            transform.position+= Vector3.down*0.035f;
        }
        if (Input.GetKey(leftKey)){
            transform.position+= Vector3.left*0.035f;
        }
        if (Input.GetKey(rightKey)){
            transform.position+= Vector3.right*0.035f;
        }
    }
}
