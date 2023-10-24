using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        
        transform.position+= Vector3.left*0.04f;
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name=="Monster"){
            Destroy(gameObject);
        }
    }
}
