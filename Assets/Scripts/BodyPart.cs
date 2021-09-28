using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    private GameMaster gm;
    private SpriteRenderer sr;
    private float time;
    private float cycleSpeed;
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
        }
        if(time >= cycleDuration * snakeSize - cycleDuration && isTail == false)
        {
            isTail = true;
            //to do: put on tail sprite
        }
    }
}
