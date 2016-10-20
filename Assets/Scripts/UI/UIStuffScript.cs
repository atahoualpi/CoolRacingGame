﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UIStuffScript : MonoBehaviour {

    private Text lapText;
    private Text rankText;
    private Text timeText;

    public Image PUimage;
    public GameLogic gameLogic;

    // Use this for initialization
    void Awake () {
        PUimage = transform.FindChild("PowerUpImage").GetComponent<Image>();
    }

    void Start()
    {
        timeText = transform.FindChild("TimeText").GetComponent<Text>();
        rankText = transform.FindChild("RankText").GetComponent<Text>();

    }
    // Update is called once per frame
    void Update () {
        //timeText.text = Math.Round((Decimal)gameLogic.t, 2).ToString().Replace(".",":");
        timeText.text = NiceFormat(gameLogic.t);
        int playerpos = -1;
        List<GameObject> cars = gameLogic.rankCars();
        for(int i = 0; i < cars.Count; i++)
        {
            if(cars[i].tag == "ActualVehicle")
            {
                playerpos = i+1;
                break;
            }
        }
        rankText.text = playerpos + "/" + cars.Count;

    }

    string NiceFormat(float totalSeconds)
    {
        int seconds = (int)totalSeconds % 60;
        int minutes = (int)totalSeconds / 60;
        string time;
        if (seconds < 10)
        {
            time = minutes + ":0" + seconds;

        }
        else
        {
            time = minutes + ":" + seconds;
        }
        return time;
    }
}
