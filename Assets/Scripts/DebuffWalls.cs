using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffWalls : MonoBehaviour
{
    public GameMaster gm;
    private float time;
    private float cycleSpeed;
    private float cycleDuration;
    public int wallDuration;

    void Start()
    {
        cycleSpeed = gm.cycleSpeed;
        cycleDuration = gm.cycleDuration;
        time = 0;
    }
    void Update()
    {
        time = time + cycleSpeed * Time.deltaTime;
        if(time >= cycleDuration * wallDuration)
        {
            gameObject.SetActive(false);
        }
    }
}
