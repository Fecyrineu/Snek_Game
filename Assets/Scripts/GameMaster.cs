using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public float cycleDuration;
    public float cycleSpeed;
    private float time;
    public int snakeSize;
    public float gridSize;
    public int points;

    public int pointsForNormalFood;
    public int pointsForBuff;
    public int pointsForDebuff;

    public int speedIncreaseUponEating;
    public int speedIncreaseUponDebuff;
    public int speedDecreaseUponBuff;
    public int sizeIncreaseUponEating;
    public int sizeIncreaseUponDebuff;
    public int sizeDecreaseUponBuff;


    public GameObject snakeHead;
    public GameObject randomSpawnFood;
    public GameObject randomSpawnBuff;
    public GameObject randomSpawnDebuff;
    public GameObject pointsUI;
    private Text _pointsUI;

    public GameObject bodyPartHorizontal;
    public GameObject bodyPartVertical;
    public GameObject bodyPartUpRight;
    public GameObject bodyPartRightDown;
    public GameObject bodyPartDownLeft;
    public GameObject bodyPartLeftUp;

    public bool movingUp = true;
    public bool movingRight = false;
    public bool movingDown = false;
    public bool movingLeft = false;
    private bool hasMovedThisCycle = false;
    private bool hasMovedRight = false;
    private bool hasMovedLeft = false;

    public UnityEvent wallExpansion;
    public UnityEvent onGameEnd;

    void Start()
    {
        _pointsUI = pointsUI.GetComponent<Text>();
    }

    void Update()
    {
        time = time + cycleSpeed * Time.deltaTime;
        if(time >= cycleDuration)
        {
            moveAhead();
            time = 0;
            hasMovedThisCycle = false;
            hasMovedRight = false;
            hasMovedLeft = false;
        }
    }

    public void turnRight()
    {
        if(hasMovedThisCycle == false)
        {
            if(movingUp == true){movingUp = false; movingRight = true;}
            if(movingRight == true){movingRight = false; movingDown = true;}
            if(movingDown == true){movingDown = false; movingLeft = true;}
            if(movingLeft == true){movingLeft = false; movingUp = true;}
            hasMovedThisCycle = true;
            hasMovedRight = true;
        }
    }
    public void turnLeft()
    {
        if(hasMovedThisCycle == false)
        {
            if(movingUp == true){movingUp = false; movingLeft = true;}
            if(movingRight == true){movingRight = false; movingUp = true;}
            if(movingDown == true){movingDown = false; movingRight = true;}
            if(movingLeft == true){movingLeft = false; movingDown = true;}
            hasMovedThisCycle = true;
            hasMovedLeft = true;
        }
    }
    public void moveAhead()
    {
        if(hasMovedLeft == false && hasMovedRight == false)
        {
            if(movingUp == true || movingDown == true)
            {
                Instantiate(bodyPartVertical, snakeHead.transform.position, Quaternion.identity);
            }
            if(movingRight == true || movingLeft == true)
            {
                Instantiate(bodyPartHorizontal, snakeHead.transform.position, Quaternion.identity);
            }
        }
        else
        {
            if(movingUp == true && hasMovedLeft == true || movingRight == true && hasMovedRight == true)
            {
                Instantiate(bodyPartUpRight, snakeHead.transform.position, Quaternion.identity);
            }
            if(movingRight == true && hasMovedLeft == true || movingDown == true && hasMovedRight == true)
            {
                Instantiate(bodyPartRightDown, snakeHead.transform.position, Quaternion.identity);
            }
            if(movingDown == true && hasMovedLeft == true || movingLeft == true && hasMovedRight == true)
            {
                Instantiate(bodyPartDownLeft, snakeHead.transform.position, Quaternion.identity);
            }
            if(movingLeft == true && hasMovedLeft == true || movingUp == true && hasMovedRight == true)
            {
                Instantiate(bodyPartLeftUp, snakeHead.transform.position, Quaternion.identity);
            }
        }

        if(movingUp == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(0, gridSize, 0);}
        if(movingRight == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(gridSize, 0, 0);}
        if(movingDown == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(0, -gridSize, 0);}
        if(movingLeft == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(-gridSize, 0, 0);}
    }
    public void endGame()
    {
        onGameEnd.Invoke();
    }
    public void addSize()
    {
        snakeSize = snakeSize + speedIncreaseUponEating;
    }
    public void addSizeDebuff()
    {
        snakeSize = snakeSize + sizeIncreaseUponDebuff;
    }
    public void removeSizeBuff()
    {
        snakeSize = snakeSize - sizeDecreaseUponBuff;
    }
    public void addSpeed()
    {
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
    }
    public void addSpeedDebuff()
    {
        cycleSpeed = cycleSpeed + speedIncreaseUponDebuff;
    }
    public void removeSpeedBuff()
    {
        cycleSpeed = cycleSpeed - speedDecreaseUponBuff;
    }
    public void normalPoints()
    {
        points = points + pointsForNormalFood;
        _pointsUI.text = "Points: " + points;
    }
    public void buffPoints()
    {
        points = points + pointsForBuff;
        _pointsUI.text = "Points: " + points;
    }
    public void debuffPoints()
    {
        points = points + pointsForDebuff;
        _pointsUI.text = "Points: " + points;
    }
}
