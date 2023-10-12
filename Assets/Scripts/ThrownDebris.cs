using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownDebris : MonoBehaviour
{
    public Vector3 target;
    public GameObject log;
    int numDebris = 1;
    GameObject[] debris = new GameObject[1];
    Vector3[] targets = new Vector3[1];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numDebris; i++){
            targets[i] = new Vector3(0f, 0f, 0f);
            debris[i] = Instantiate(log, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numDebris; i++){
            debris[i].transform.position += targets[i].normalized*0.05f;
        }
    }

    public void setTarget(int debrisIndex, Vector3 newTarget)
    {
        targets[debrisIndex] = newTarget;
    }
}
