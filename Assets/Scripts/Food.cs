using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float foodDurationInCycles;
    private int cycles;

    private float time;
    private float cycleSpeed;
    private float cycleDuration;
    private GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        cycleSpeed = gm.cycleSpeed;
        cycleDuration = gm.cycleDuration;
    }

    void Update()
    {
        time = time + cycleSpeed * Time.deltaTime;
        if(time >= cycleDuration)
        {
            time = 0;
            cycles++;
            if(cycles >= foodDurationInCycles)
            {
                Destroy(gameObject);
            }
        }
    }
}
