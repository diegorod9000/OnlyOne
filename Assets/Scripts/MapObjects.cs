using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjects : MonoBehaviour
{
    public GameObject log; 
    public GameObject tree; 
    public ThrownDebris debrisScript;
    int framePos = 0;
    const int numBorders = 30;
    const int numObstacles = 50;
    int currentObstacle = 0;
    GameObject[] borders = new GameObject[numBorders];
    GameObject[] obstacles = new GameObject[numObstacles];
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(log, new Vector3(2.0f, 0, 0), Quaternion.identity);
        for(int i = 0; i < numBorders; i += 2){
            float x_pos = -40.0f + (90.0f / ((float)numBorders)) * i;
            borders[i] = Instantiate(log, new Vector3(x_pos, 23.0f, 0), new Quaternion(180,0,0,0));
            borders[i + 1] = Instantiate(log, new Vector3(x_pos, -23.0f, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.frozen){
            return;
        }
        
        if(framePos == 300){
            framePos = 0;
            Destroy(borders[0]);
            Destroy(borders[1]);
            for(int i = 0; i < numBorders - 2; i+=2){
                borders[i] = borders[i + 2];
                borders[i + 1] = borders[i + 3];
            }
            borders[numBorders - 1] = Instantiate(log, new Vector3(44.0f, 23.0f, 0), new Quaternion(180,0,0,0));
            borders[numBorders - 2] = Instantiate(log, new Vector3(44.0f, -23.0f, 0), Quaternion.identity);
        }
        int numDeleted = 0;
        for(int i = 0; i < numObstacles; i++){
            if(obstacles[i] && obstacles[i].transform.position.x < -50.0f){
                Destroy(obstacles[i]);
                numDeleted++;
            }
        }
        if(numDeleted > 0){
            currentObstacle -= numDeleted;
            for(int i = 0; i < numObstacles - numDeleted; i++){
                obstacles[i] = obstacles[i + numDeleted];
            }
        }
        float rand = Random.Range(0.0f, 2.0f);
        if(framePos % 100 == 0  && rand < 1.0f && currentObstacle != numObstacles){
            if(rand < 0.33f){
                obstacles[currentObstacle] = Instantiate(log, new Vector3(50.0f, Random.Range(-19.0f, 19.0f), 0), new Quaternion(Random.Range(0,180),Random.Range(0,180),0,0));
                currentObstacle++;
            } else if(rand < 0.66f) {
                obstacles[currentObstacle] = Instantiate(tree, new Vector3(50.0f, Random.Range(-15.0f, 15.0f), 0), Quaternion.identity);
                currentObstacle++;
            } else {
                debrisScript.addDebris(new Vector3(50.0f, Random.Range(-19.0f, 19.0f), 0));
            }
        }
        framePos ++;
    }
}
