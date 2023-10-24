using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour 
{
    // Start is called before the first frame update
    public KeyCode upKey, downKey, leftKey, rightKey, throwKey;
    public ThrownDebris debrisScript;
    public GameObject player2;
    public Button start;
    bool isStarted = false;
    bool up, down, left, right;
    bool stun;
    int stunClock;
    const int stunAmt = 1000;
    int lastThrown = -1;
    public int playerNum;
    public Text winText;

    private float playerSpeed;
    private float adrenaline;

    private int boostTime;

    public SpriteRenderer currentSprite;
    public Sprite upSprite, downSprite, leftSprite, rightSprite, stunSprite;

    public AudioClip walkingSound;
    public AudioSource walkingSource;
    void Start()
    {
        adrenaline = 0.0f;
        playerSpeed = 0.05f;
        boostTime = 0;

        currentSprite = GetComponent<SpriteRenderer>();
        walkingSource = GetComponent<AudioSource>();
        stun = false;
        stunClock = stunAmt;

        start.onClick.AddListener(Begin);

        up = false;
        left = false;
        right = false;
        down = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(GameManager.frozen){
            return;
        }

        Vector3 positionMove = Vector3.zero;
        if(isStarted){
            transform.position+= Vector3.left*0.04f;
        }
        if(stun){
            currentSprite.sprite=stunSprite;
            stunClock--;
            if(stunClock == 0){
                stun = false;
                stunClock = stunAmt;
            }
        }
        else {
            if (Input.GetKey(upKey)){
                up = true;
                currentSprite.sprite = upSprite;
                positionMove+= Vector3.up;
            } else {
                up = false;
            }
            if (Input.GetKey(downKey)){
                down = true;
                currentSprite.sprite = downSprite;
                positionMove+= Vector3.down;
            } else {
                down = false;
            }
            if (Input.GetKey(leftKey)){
                left = true;
                currentSprite.sprite = leftSprite;
                positionMove+= Vector3.left;
            } else {
                left = false;
            }
            if (Input.GetKey(rightKey)){
                right = true;
                if(currentSprite.sprite!=rightSprite){
                    currentSprite.sprite = rightSprite;
                }
                positionMove+= Vector3.right;
            } else {
                right = false;
            }
            if (Input.GetKey(throwKey)){
                for(int i = 0; i < debrisScript.getNumDebris(); i++){
                    GameObject debris = debrisScript.getDebris(i);
                    if(debris && vecClose(debris.transform.position - transform.position)){
                        Vector3 newTarget = player2.transform.position - transform.position;
                        if(up) newTarget += Vector3.up*0.02f;
                        if(left) newTarget += Vector3.left*0.02f;
                        if(down) newTarget += Vector3.down*0.02f;
                        if(right) newTarget += Vector3.right*0.02f;
                        debrisScript.setTarget(i,newTarget);
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

        if(transform.position.x<-10)
        {
            adrenaline+=0.1f;
        }
        else 
        {
            adrenaline -=0.05f;
        }
        if(adrenaline > 120)
        {
            boostTime = 1200;
        }
        if(boostTime>0)
        {
            playerSpeed = 0.06f;
            boostTime--;
        }
        else
        {
            playerSpeed = 0.05f;
        }
    }

    bool vecClose(Vector3 vec){
        if(vec.x * vec.x < 3 && vec.y * vec.y < 7){
            return true;
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name=="Monster"){
            if(playerNum == 1){
                winText.text = "Player 2 wins";
            } else {
                winText.text = "Player 1 wins";
                winText.color = Color.blue;
            }
            Debug.Log("You died");
            GameManager.frozen = true;
        }
    }

    void Begin(){
        isStarted = true;
    }


}
