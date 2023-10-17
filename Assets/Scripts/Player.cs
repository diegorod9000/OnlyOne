using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour 
{
    // Start is called before the first frame update
    public KeyCode upKey, downKey, leftKey, rightKey, throwKey;
    public ThrownDebris debrisScript;
    public GameObject player2;
    bool stun;
    int stunClock;
    const int stunAmt = 1000;
    int lastThrown = -1;

    private float playerSpeed;
    private float adrenaline;

    public SpriteRenderer currentSprite;
    public Sprite upSprite,downSprite,leftSprite,rightSprite;

    public AudioClip walkingSound;
    public AudioSource walkingSource;
    void Start()
    {
        adrenaline = 0.0f;
        playerSpeed = 0.03f;
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
                positionMove+= Vector3.up;
            }
            if (Input.GetKey(downKey)){
                currentSprite.sprite = downSprite;
                positionMove+= Vector3.down;
            }
            if (Input.GetKey(leftKey)){
                currentSprite.sprite = leftSprite;
                positionMove+= Vector3.left;
            }
            if (Input.GetKey(rightKey)){
                if(currentSprite.sprite!=rightSprite){
                    currentSprite.sprite = rightSprite;
                }
                positionMove+= Vector3.right;
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
                positionMove = positionMove.normalized*playerSpeed;
                
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
        if(transform.position.x<-10){
            adrenaline+=0.1f;
        }
    }

    bool vecClose(Vector3 vec){
        if(vec.x * vec.x + vec.y * vec.y < 5){
            return true;
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name=="Monster"){
            Debug.Log("You died");
        }
    }


}
