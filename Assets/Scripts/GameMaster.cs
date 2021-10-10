using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    //code that prevents wall expansion debuff from spawining at the edges is hardcoded. Fix later, maybe.

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
    public GameObject randomSpawn;
    private RandySpawner rs;
    public GameObject pointsUI;
    private Text _pointsUI;

    public GameObject bodyPartHorizontal;
    public GameObject bodyPartVertical;
    public GameObject bodyPartUpRight;
    public GameObject bodyPartRightDown;
    public GameObject bodyPartDownLeft;
    public GameObject bodyPartLeftUp;

    public GameObject rock;

    public GameObject regularFood;
    public GameObject rockBuffObject;
    public GameObject rockDebuffObject;
    public GameObject sizeBuffObject;
    public GameObject sizeDebuffObject;
    public GameObject speedBuffObject;
    public GameObject speedDebuffObject;
    public GameObject bodyBuffObject;
    public GameObject wallDebuffObject;

    public bool movingUp = true;
    public bool movingRight = false;
    public bool movingDown = false;
    public bool movingLeft = false;
    private bool hasMovedThisCycle = false;
    private bool hasMovedRight = false;
    private bool hasMovedLeft = false;
    [HideInInspector] public bool callTail;

    private GameObject[] allBodyParts;
    private GameObject nowTail;
    private GameObject aRock;

    [SerializeField] private int spawnInterval;
    private int cyclesSinceSpawn;
    [SerializeField]private int chanceToSpawnRegularFood;
    private int randy;
    private int numberOfRocks;

    public UnityEvent wallExpansion;
    public UnityEvent onGameEnd;

    public UnityEvent onEatingRegular;
    public UnityEvent onEatingBuff;
    public UnityEvent onEatingDebuff;

    void Start()
    {
        _pointsUI = pointsUI.GetComponent<Text>();
        cyclesSinceSpawn = spawnInterval;
        rs = randomSpawn.GetComponent<RandySpawner>();
    }

    void Update()
    {
        if(callTail == true)
        {
            callTail = false;
            GameObject.FindGameObjectWithTag("BodyPart").GetComponent<BodyPart>().becomeTail();
        }

        time = time + cycleSpeed * Time.deltaTime;
        if(time >= cycleDuration)
        {
            if(bodyBuffActive == true)
            {
                bodyBuffActiveFor++;
                if(bodyBuffActiveFor >= bodyBuffDuration)
                {
                    bodyBuffActive = false;
                    callTail = true;
                }
            }

            moveAhead();
            time = 0;
            hasMovedThisCycle = false;
            hasMovedRight = false;
            hasMovedLeft = false;

            cyclesSinceSpawn++;
            if(cyclesSinceSpawn >= spawnInterval)
            {
                cyclesSinceSpawn = 0;
                randy = Random.Range(1, chanceToSpawnRegularFood + 9);
                switch (randy)
                {
                    case 1:
                        if(numberOfRocks > 0)
                        {
                            Instantiate(rockBuffObject, randomSpawn.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(regularFood, randomSpawn.transform.position, Quaternion.identity);
                        }
                        break;
                    case 2:
                        Instantiate(rockDebuffObject, randomSpawn.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        if(snakeSize > sizeDecreaseUponBuff)
                        {
                            Instantiate(sizeBuffObject, randomSpawn.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(regularFood, randomSpawn.transform.position, Quaternion.identity);
                        }
                        break;
                    case 4:
                        Instantiate(sizeDebuffObject, randomSpawn.transform.position, Quaternion.identity);
                        break;
                    case 5:
                        if(cycleSpeed >= cycleDuration + speedDecreaseUponBuff)
                        {
                            Instantiate(speedBuffObject, randomSpawn.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(regularFood, randomSpawn.transform.position, Quaternion.identity);
                        }
                        break;
                    case 6:
                        Instantiate(speedDebuffObject, randomSpawn.transform.position, Quaternion.identity);
                        break;
                    case 7:
                        if(randomSpawn.transform.position.x <= rs.minimunXRange * rs.gridSize + rs.XOffset || randomSpawn.transform.position.x >= rs.maximumXRange * rs.gridSize + rs.XOffset || randomSpawn.transform.position.y <= rs.minimunYRange * rs.gridSize + rs.YOffset || randomSpawn.transform.position.y >= rs.maximumYRange * rs.gridSize + rs.YOffset)
                        {
                            Instantiate(regularFood, randomSpawn.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(wallDebuffObject, randomSpawn.transform.position, Quaternion.identity);
                        }
                        break;
                    case 8:
                        Instantiate(bodyBuffObject, randomSpawn.transform.position, Quaternion.identity);
                        break;
                    default:
                        Instantiate(regularFood, randomSpawn.transform.position, Quaternion.identity);
                        break;
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
        if(movingUp == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(0, gridSize, 0); snakeHead.transform.rotation = Quaternion.Euler(0, 0, 0);}
        if(movingRight == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(gridSize, 0, 0); snakeHead.transform.rotation = Quaternion.Euler(0, 0, 270);}
        if(movingDown == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(0, -gridSize, 0); snakeHead.transform.rotation = Quaternion.Euler(0, 0, 180);}
        if(movingLeft == true){snakeHead.transform.position = snakeHead.transform.position + new Vector3(-gridSize, 0, 0); snakeHead.transform.rotation = Quaternion.Euler(0, 0, 90);}
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
        onEatingRegular.Invoke();
    }
    public void speedBuff()
    {
        points = points + pointsForBuff;
        snakeSize = snakeSize + sizeIncreaseUponEating;
        cycleSpeed = cycleSpeed - speedDecreaseUponBuff;
        onEatingBuff.Invoke();
    }
    public void speedDebuff()
    {
        points = points + pointsForDebuff;
        snakeSize = snakeSize + sizeIncreaseUponEating;
        cycleSpeed = cycleSpeed + speedIncreaseUponDebuff;
        onEatingDebuff.Invoke();
    }
    public void sizedBuff()
    {
        points = points + pointsForBuff;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
        snakeSize = snakeSize - sizeDecreaseUponBuff;
        onEatingBuff.Invoke();
    }
    public void sizeDebuff()
    {
        points = points + pointsForDebuff;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
        snakeSize = snakeSize + sizeIncreaseUponDebuff;
        onEatingDebuff.Invoke();
    }
    public void wallsDebuff()
    {
        points = points + pointsForDebuff;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
        snakeSize = snakeSize + sizeIncreaseUponEating;
        onEatingDebuff.Invoke();
        wallExpansion.Invoke();
    }
    public void bodyBuff()
    {
        points = points + pointsForBuff;
        snakeSize = snakeSize + sizeIncreaseUponEating;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
        onEatingBuff.Invoke();

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
        onEatingDebuff.Invoke();

        numberOfRocks++;

        Instantiate(rock, randomSpawn.transform.position, Quaternion.identity);
    }
    public void rockBuff()
    {
        points = points + pointsForBuff;
        cycleSpeed = cycleSpeed + speedIncreaseUponEating;
        snakeSize = snakeSize + sizeIncreaseUponEating;
        onEatingBuff.Invoke();

        numberOfRocks--;

        aRock = GameObject.FindWithTag("Rock");
        Destroy(aRock);
    }
}
