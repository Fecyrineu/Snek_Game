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
        if(other.gameObject.tag == "BodyPart" || other.gameObject.tag == "Wall" || other.gameObject.tag == "Rock")
        {
            gm.endGame();
        }
        if(other.gameObject.tag == "GenericFood")
        {
            gm.genericFood();
            Destroy(other);
        }
        if(other.gameObject.tag == "SpeedBuff")
        {
            gm.speedBuff();
            Destroy(other);
        }
        if(other.gameObject.tag == "SizeBuff")
        {
            gm.sizedBuff();
            Destroy(other);
        }
        if(other.gameObject.tag == "SpeedDebuff")
        {
            gm.speedDebuff();
            Destroy(other);
        }
        if(other.gameObject.tag == "SizeDebuff")
        {
            gm.sizeDebuff();
            Destroy(other);
        }
        if(other.gameObject.tag == "WallDebuff")
        {
            gm.wallsDebuff();
            Destroy(other);
        }
        if(other.gameObject.tag == "BodyBuff")
        {
            gm.bodyBuff();
            Destroy(other);
        }
        if(other.gameObject.tag == "RockDebuff")
        {
            gm.rockDebuff();
            Destroy(other);
        }
    }
}
