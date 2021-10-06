using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandySpawner : MonoBehaviour
{
    public GameObject snakeHead;
    public float gridSize;
    public float XOffset;
    public float YOffset;

    public int minimunXRange;
    public int maximumXRange;
    public int minimunYRange;
    public int maximumYRange;
    private int x, y;
    private float x2, y2;

    void Start()
    {
        RandomLocation();
    }

    void Update()
    {
        if(snakeHead.transform.position == gameObject.transform.position)
        {
            RandomLocation();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        RandomLocation();
    }
    void OnCollisionStay2D(Collision2D other)
    {
        RandomLocation();
    }

    void RandomLocation()
    {
        x = Random.Range(minimunXRange, maximumXRange + 1);
        y = Random.Range(minimunYRange, maximumYRange + 1);
        x2 = x * gridSize;
        y2 = x * gridSize;
        gameObject.transform.position = new Vector3(x2 + XOffset, y2 + YOffset, 0);
    }
}
