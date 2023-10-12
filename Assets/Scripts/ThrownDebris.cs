using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownDebris : MonoBehaviour
{
    public Vector3 target;
    public GameObject log;
    public GameObject player1;
    public GameObject player2;
    const int numDebris = 1;
    GameObject[] debris = new GameObject[numDebris];
    Vector3[] targets = new Vector3[numDebris];
    bool[] thrown = new bool[numDebris];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numDebris; i++){
            targets[i] = new Vector3(0f, 0f, 0f);
            debris[i] = Instantiate(log, new Vector3(0, 0, 0), Quaternion.identity);
            thrown[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numDebris; i++){
            if(debris[i]){
                debris[i].transform.position += targets[i].normalized*0.05f;
            }
        }
    }

    public void setTarget(int debrisIndex, Vector3 newTarget)
    {
        targets[debrisIndex] = newTarget;
    }

    public GameObject getDebris(int i){
        return debris[i];
    }

    public void throwDebris(int i){
        thrown[i] = true;
    }

    public bool debrisThrown(int i){
        return thrown[i];
    }

    public int getNumDebris(){
        return numDebris;
    }

    public void destroyDebris(int i){
        Destroy(debris[i]);
        targets[i] = new Vector3(0f, 0f, 0f);
        thrown[i] = false;
    }
}
