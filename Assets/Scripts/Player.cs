using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour 
{
    // Start is called before the first frame update
    public KeyCode upKey, downKey, leftKey, rightKey, throwKey;
    public ThrownDebris debrisScript;
    public GameObject player2;
    static bool frozen;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionMove = Vector3.zero;
        transform.position+= Vector3.left*0.02f;
        if (Input.GetKey(upKey)){
            positionMove+= Vector3.up*0.035f;
        }
        if (Input.GetKey(downKey)){
            positionMove+= Vector3.down*0.035f;
        }
        if (Input.GetKey(leftKey)){
            positionMove+= Vector3.left*0.035f;
        }
        if (Input.GetKey(rightKey)){
            positionMove+= Vector3.right*0.035f;
        }
        if (Input.GetKey(throwKey)){
            debrisScript.setTarget(0,new Vector3(0.5f, 0.5f, 0));
        }
        if(positionMove.magnitude > 0)
        {
            positionMove = positionMove.normalized*0.035f;
        }
        transform.position+=positionMove;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name=="Monster"){
            Debug.Log("You died");
            frozen = true;
        }
    }
}
