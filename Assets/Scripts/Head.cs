using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public GameObject gameMasterObject;
    private GameMaster gm;
    private Rigidbody2D rb;

    void Start()
    {
        gm = gameMasterObject.GetComponent<GameMaster>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "BodyPart")
        {
            gm.endGame();
        }
        if(other.gameObject.tag == "Wall")
        {
            gm.endGame();
        }
        if(other.gameObject.tag == "GenericFood")
        {
            gm.points++;
            gm.addSize();
            gm.addSpeed();
            gm.normalPoints();
            Destroy(other);
        }
        if(other.gameObject.tag == "SpeedBuff")
        {
            gm.points++;
            gm.addSize();
            gm.addSpeed();
            gm.normalPoints();
            gm.removeSpeedBuff();
            Destroy(other);
        }
        if(other.gameObject.tag == "SizeBuff")
        {
            gm.points++;
            gm.addSize();
            gm.addSpeed();
            gm.normalPoints();
            gm.removeSizeBuff();
            Destroy(other);
        }
        if(other.gameObject.tag == "SpeedDebuff")
        {
            gm.points++;
            gm.addSize();
            gm.addSpeed();
            gm.normalPoints();
            gm.addSpeedDebuff();
            Destroy(other);
        }
        if(other.gameObject.tag == "SizeDebuff")
        {
            gm.points++;
            gm.addSize();
            gm.addSpeed();
            gm.normalPoints();
            gm.addSizeDebuff();
            Destroy(other);
        }
        
    }
}
