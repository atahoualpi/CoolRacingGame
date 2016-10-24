using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UIStuffScript : MonoBehaviour {

    private Text lapText;
    private Text rankText;
    private Text timeText;
    private Text goText;
    private Text elimText;

    public Image PUimage;
    public GameLogic gameLogic;
    public WayPointsScript player_wps;
    int lapCount;

    private CanvasGroup winImage;
    private CanvasGroup loseImage;
    private bool won;
    float time;
    public AudioSource loseAudio;
    public AudioSource winAudio;

    // Use this for initialization
    void Awake () {
        PUimage = transform.FindChild("PowerUpImage").GetComponent<Image>();
        Time.timeScale = 1;
    }

    void Start()
    {
        won = false;
        lapCount = player_wps.lapCount;
        timeText = transform.FindChild("TimeText").GetComponent<Text>();
        rankText = transform.FindChild("RankText").GetComponent<Text>();
        lapText = transform.FindChild("LapText").GetComponent<Text>();
        goText = transform.FindChild("GOText").GetComponent<Text>();
        elimText = transform.FindChild("EliminationText").GetComponent<Text>();

        winImage = transform.FindChild("WinImage").GetComponent<CanvasGroup>();
        loseImage = transform.FindChild("LoseImage").GetComponent<CanvasGroup>();
        time = 4;
        elimText.text = "";
    }

    private IEnumerator sec1point5()
    {
        yield return new WaitForSeconds(1.5f);
        goText.text = "";
    }

    private IEnumerator sec2()
    {
        yield return new WaitForSeconds(2f);
        gameLogic.carName = null;
        elimText.text = "";
    }

    // Update is called once per frame
    void Update () {
        if (!gameLogic.start)
        {
            time -= Time.deltaTime;
            if ((int)time > 0)
                goText.text = "" + (int)time;
            else
            {
                goText.color = Color.green;
                goText.text = "GO";
                StartCoroutine(sec1point5());
            }
        }

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
        lapText.text = "Lap: " + player_wps.currentLap + "/" + lapCount;

        if (gameLogic.carName != null)
        {
            elimText.text = gameLogic.carName + " eliminated";
            StopCoroutine(sec2());
            StartCoroutine(sec2());
        }

        if (gameLogic.isTimeMode)
        {
            if(cars.Count == 1 && playerpos == 1)
            {
                WinScreen();
                Time.timeScale = 0;
            }
            else if (!gameLogic.won)
            {
                LoseScreen();
            }
        }
        else
        {
            if (player_wps.currentLap > lapCount)
            {
                if (gameLogic.getFirstCar() == "Player")
                {
                    WinScreen();
                    Debug.Log("YOU WIN");
                }
                else {
                    Debug.Log("YOU LOSE");

                    LoseScreen();
                }
            }
        }
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

    public void updateLap(int curLap) {
        lapText.text = "Lap: " + curLap + "/" + lapCount;
    }

    void WinScreen()
    {
        winImage.alpha = 1;
        won = true;
        winAudio.Play();
        Time.timeScale = 0;
    }

    void LoseScreen()
    {

        if (!won)
        {
            loseImage.alpha = 1;
            loseAudio.Play();
        }

        Time.timeScale = 0;
    }
}
