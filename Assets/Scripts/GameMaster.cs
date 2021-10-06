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

    public int bodyBuffDuration;
    private bool bodyBuffActive = false;
    private int bodyBuffActiveFor;


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

    public GameObject rock;

    public bool movingUp = true;
    public bool movingRight = false;
    public bool movingDown = false;
    public bool movingLeft = false;
    private bool hasMovedThisCycle = false;
    private bool hasMovedRight = false;
    private bool hasMovedLeft = false;

    private GameObject[] allBodyParts;

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
            if(bodyBuffActive == true)
            {
                bodyBuffActiveFor++;
                if(bodyBuffActiveFor >= bodyBuffDuration)
                {
                    bodyBuffActive = false;
                }
            }
        }
    }

    public void turnRight()
    {
        if(hasMovedThisCycle == false)
        {
            if(movingUp == true && hasMovedThisCycle == false){movingUp = false; movingRight = true; hasMovedThisCycle = true;}
            if(movingRight == true && hasMovedThisCycle == false){movingRight = false; movingDown = true; hasMovedThisCycle = true;}
            if(movingDown == true && hasMovedThisCycle == false){movingDown = false; movingLeft = true; hasMovedThisCycle = true;}
            if(movingLeft == true && hasMovedThisCycle == false){movingLeft = false; movingUp = true; hasMovedThisCycle = true;}
            hasMovedRight = true;
        }
    }
    public void turnLeft()
    {
        if(hasMovedThisCycle == false)
        {
            if(movingUp == true && hasMovedThisCycle == false){movingUp = false; movingLeft = true; hasMovedThisCycle = true;}
            if(movingRight == true && hasMovedThisCycle == false){movingRight = false; movingUp = true; hasMovedThisCycle = true;}
            if(movingDown == true && hasMovedThisCycle == false){movingDown = false; movingRight = true; hasMovedThisCycle = true;}
            if(movingLeft == true && hasMovedThisCycle == false){movingLeft = false; movingDown = true; hasMovedThisCycle = true;}
            hasMovedLeft = true;
        }
    }
    public void moveAhead()
    {
        if(bodyBuffActive == false)
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
        }
        if(movingUp == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(0, gridSize, 0);}
        if(movingRight == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(gridSize, 0, 0);}
        if(movingDown == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(0, -gridSize, 0);}
        if(movingLeft == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(-gridSize, 0, 0);}
    }
    public void endGame()
    {
        onGameEnd.Invoke();
        allBodyParts = GameObject.FindGameObjectsWithTag("BodyPart");
        foreach(GameObject bodyPart in allBodyParts)
        {
            bodyPart.GetComponent<BodyPart>().enabled = false;
        }
    }
    public void genericFood()
    {
        points = points + pointsForNormalFood;
        snakeSize = snakeSize + sizeIncreaseUponEating;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
    }
    public void speedBuff()
    {
        points = points + pointsForBuff;
        snakeSize = snakeSize + sizeIncreaseUponEating;
        cycleSpeed = cycleSpeed + speedDecreaseUponBuff;
    }
    public void speedDebuff()
    {
        points = points + pointsForDebuff;
        snakeSize = snakeSize + sizeIncreaseUponEating;
        cycleSpeed = cycleSpeed + speedIncreaseUponDebuff;
    }
    public void sizedBuff()
    {
        points = points + pointsForBuff;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
        snakeSize = snakeSize + sizeDecreaseUponBuff;
    }
    public void sizeDebuff()
    {
        points = points + pointsForDebuff;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
        snakeSize = snakeSize + sizeIncreaseUponDebuff;
    }
    public void wallsDebuff()
    {
        points = points + pointsForDebuff;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
        snakeSize = snakeSize + sizeIncreaseUponEating;
        wallExpansion.Invoke();
    }
    public void bodyBuff()
    {
        points = points + pointsForBuff;
        snakeSize = snakeSize + sizeIncreaseUponEating;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;

        allBodyParts = GameObject.FindGameObjectsWithTag("BodyPart");
        foreach(GameObject bodyPart in allBodyParts)
        {
            Destroy(bodyPart);
        }
        bodyBuffActive = true;
        bodyBuffActiveFor = 0;
    }
    public void rockDebuff()
    {
        points = points + pointsForDebuff;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
        snakeSize = snakeSize + sizeIncreaseUponEating;

        Instantiate(rock, randomSpawnDebuff.transform.position, Quaternion.identity);
    }
}
