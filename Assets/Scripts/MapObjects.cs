using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjects : MonoBehaviour
{
    public GameObject log; 
    float framePos = 0.1f;
    const int numBorders = 10;
    GameObject[] borders = new GameObject[numBorders];
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(log, new Vector3(2.0f, 0, 0), Quaternion.identity);
        borders[numBorders - 1] = Instantiate(log, new Vector3(0, 23.0f, 0), new Quaternion(180,0,0,0));
        borders[numBorders - 2] = Instantiate(log, new Vector3(0, -23.0f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(framePos % 6 < 0.01){
            Destroy(borders[0]);
            Destroy(borders[1]);
            for(int i = 0; i < numBorders - 2; i+=2){
                borders[i] = borders[i + 2];
                borders[i + 1] = borders[i + 3];
            }
            borders[numBorders - 1] = Instantiate(log, new Vector3(0, 23.0f, 0), new Quaternion(180,0,0,0));
            borders[numBorders - 2] = Instantiate(log, new Vector3(0, -23.0f, 0), Quaternion.identity);
        }
        framePos += 0.02f;
    }
}
