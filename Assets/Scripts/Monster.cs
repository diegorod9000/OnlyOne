using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other) {
        Debug.Log("collided");
        if(other.gameObject.CompareTag("Player1")){
            Application.Quit();
        }
        else if(other.gameObject.CompareTag("Player2")){
            Application.Quit();
        }
        Application.Quit();
    }
}