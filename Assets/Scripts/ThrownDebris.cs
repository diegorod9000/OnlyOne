using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownDebris : MonoBehaviour
{
    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+= target.normalized*0.5f;
    }
}
