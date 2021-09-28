using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    public GameObject gameMasterObject;
    private GameMaster gm;

    public GameObject pointsUI;
    private Text _pointsUI;
    public GameObject sizeUI;
    private Text _sizeUI;
    void Start()
    {
        gm = gameMasterObject.GetComponent<GameMaster>();
        _pointsUI = pointsUI.GetComponent<Text>();
        _sizeUI = sizeUI.GetComponent<Text>();

        _pointsUI.text = "" + gm.points;
        _sizeUI.text = "" + gm.snakeSize;
    }
}
