using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    private GameMaster gm;
    private SpriteRenderer sr;
    private float time;
    public float cycleSpeed;
    private float cycleDuration;
    private int snakeSize;

    private bool movingUp;
    private bool movingDown;
    private bool movingRight;
    private bool movingLeft;

    private bool isTail;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        cycleSpeed = gm.cycleSpeed;
        cycleDuration = gm.cycleDuration;
        snakeSize = gm.snakeSize;
        movingUp = gm.movingUp;
        movingRight = gm.movingRight;
        movingDown = gm.movingDown;
        movingLeft = gm.movingLeft;
    }

    void Update()
    {
        time = time + cycleSpeed * Time.deltaTime;

        if(time >= cycleDuration * snakeSize)
        {
            Destroy(gameObject);
            gm.callTail = true;
        }
    }

    public void becomeTail()
    {
        sr.sprite = Resources.Load<Sprite>("SnakeTale");
            if(movingUp == true){gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);}
            if(movingRight == true){gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);}
            if(movingDown == true){gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);}
            if(movingLeft == true){gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);}
    }
}
