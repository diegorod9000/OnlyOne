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
    bool stun;
    int stunClock;
    const int stunAmt = 1000;
    int lastThrown = -1;

    public SpriteRenderer currentSprite;
    public Sprite upSprite,downSprite,leftSprite,rightSprite;

    public AudioClip walkingSound;
    public AudioSource walkingSource;
    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        walkingSource = GetComponent<AudioSource>();
        stun = false;
        stunClock = stunAmt;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionMove = Vector3.zero;
        transform.position+= Vector3.left*0.02f;
        if(stun){
            stunClock--;
            if(stunClock == 0){
                stun = false;
                stunClock = stunAmt;
            }
        }
        else {
            if (Input.GetKey(upKey)){
                currentSprite.sprite = upSprite;
                positionMove+= Vector3.up*0.035f;
            }
            if (Input.GetKey(downKey)){
                currentSprite.sprite = downSprite;
                positionMove+= Vector3.down*0.035f;
            }
            if (Input.GetKey(leftKey)){
                currentSprite.sprite = leftSprite;
                positionMove+= Vector3.left*0.035f;
            }
            if (Input.GetKey(rightKey)){
                if(currentSprite.sprite!=rightSprite){
                    currentSprite.sprite = rightSprite;
                }
                positionMove+= Vector3.right*0.035f;
            }
            if (Input.GetKey(throwKey)){
                for(int i = 0; i < debrisScript.getNumDebris(); i++){
                    GameObject debris = debrisScript.getDebris(i);
                    if(debris && vecClose(debris.transform.position - transform.position)){
                        debrisScript.setTarget(i,player2.transform.position - transform.position);
                        debrisScript.throwDebris(i);
                        lastThrown = i;
                        break;
                    }
                }
            }
            for(int i = 0; i < debrisScript.getNumDebris(); i++){
                GameObject debris = debrisScript.getDebris(i);
                if(i != lastThrown && debris && vecClose(debris.transform.position - transform.position) && debrisScript.debrisThrown(i)){
                    stun = true;
                    debrisScript.destroyDebris(i);
                    break;
                }
            }
            if(positionMove.magnitude > 0)
            {
                positionMove = positionMove.normalized*0.035f;
                
                if(!walkingSource.isPlaying)
                {
                    walkingSource.Play();
                }
            }
            else 
            {
                walkingSource.Stop();
            }
            transform.position+=positionMove;
        }
    }

    bool vecClose(Vector3 vec){
        if(vec.x * vec.x + vec.y * vec.y < 5){
            return true;
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Finish"){
            Debug.Log("You died");
            frozen = true;
        }
    }


}
