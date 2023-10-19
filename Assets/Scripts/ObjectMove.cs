using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.frozen){
            return;
        }
        
        transform.position+= Vector3.left*0.02f;
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name=="Monster"){
            Destroy(gameObject);
        }
    }
}
