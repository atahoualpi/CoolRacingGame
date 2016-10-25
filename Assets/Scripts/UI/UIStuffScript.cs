using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UIStuffScript : MonoBehaviour
{

    private Text lapText;
    private Text rankText;
    private Text timeText;
    private Text goText;
    private Text elimText;
    private Text lastLapText;

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
    public AudioSource startAudio;
    public AudioSource soundtrack;
    bool isOver;
    bool isPaused;
    private int buttonSelector;

    private Button play;
    private Button menu;
    private Button selected;
    Color highlighted;
    Color gr_btn;
    ColorBlock tempCol;

    // Use this for initialization
    void Awake()
    {
        PUimage = transform.FindChild("PowerUpImage").GetComponent<Image>();
        Time.timeScale = 1;
    }

    void Start()
    {
        isOver = false;
        won = false;
        isPaused = false;
        buttonSelector = 1;
        highlighted = new Color(0.7f, 0.96f, 0.62f, 1f);
        gr_btn = new Color(0.24f, 0.42f, 0.18f, 1f);

        lapCount = player_wps.lapCount;
        timeText = transform.FindChild("TimeText").GetComponent<Text>();
        rankText = transform.FindChild("RankText").GetComponent<Text>();
        lapText = transform.FindChild("LapText").GetComponent<Text>();
        goText = transform.FindChild("GOText").GetComponent<Text>();
        elimText = transform.FindChild("EliminationText").GetComponent<Text>();
        if (!gameLogic.isTimeMode)
            lastLapText = transform.FindChild("FinalLapText").GetComponent<Text>();

        winImage = transform.FindChild("WinImage").GetComponent<CanvasGroup>();
        loseImage = transform.FindChild("LoseImage").GetComponent<CanvasGroup>();
        time = 4;
        elimText.text = "";
        startAudio.Play();

    }

    private IEnumerator sec1point5()
    {
        yield return new WaitForSeconds(1.5f);
        goText.text = "";
        if (!gameLogic.isTimeMode)
            lastLapText.gameObject.SetActive(false);
    }

    private IEnumerator sec2()
    {
        yield return new WaitForSeconds(2f);
        gameLogic.carName = null;
        elimText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOver)
        {
            if (Input.GetKeyDown("p"))
            {
                isPaused = !isPaused;
                if (!isPaused)
                {
                    Time.timeScale = 0;
                    transform.FindChild("PauseImage").gameObject.SetActive(true);
                    soundtrack.Pause();
                }
                else
                {
                    transform.FindChild("PauseImage").gameObject.SetActive(false);
                    Time.timeScale = 1;
                    soundtrack.Play();
                }
            }
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
            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].tag == "ActualVehicle")
                {
                    playerpos = i + 1;
                    break;
                }
            }
            rankText.text = playerpos + "/" + cars.Count;


            if (gameLogic.carName != null)
            {
                elimText.text = gameLogic.carName + " eliminated";
                StopCoroutine(sec2());
                StartCoroutine(sec2());
            }

            if (gameLogic.isTimeMode)
            {

                if (cars.Count == 1 && playerpos == 1)
                {
                    WinScreen();
                }
                else if (!gameLogic.won)
                {
                        LoseScreen();
                }
            }
            else
            {
                //lapText.text = "Lap: " + player_wps.currentLap + "/" + lapCount;

                if (player_wps.currentLap == lapCount)
                {
                    lastLapText.gameObject.SetActive(true);
                    StartCoroutine(sec1point5());
                }
                if (player_wps.currentLap > lapCount)
                {
                    if (gameLogic.getFirstCar() == "Player")
                    {
                        WinScreen();
                        Debug.Log("YOU WIN");
                    }
                    else
                    {
                        Debug.Log("YOU LOSE");
                        LoseScreen();
                    }
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

    public void updateLap(int curLap)
    {
        lapText.text = "Lap: " + curLap + "/" + lapCount;
    }

    void WinScreen()
    {
        soundtrack.Stop();
        winAudio.Play();
        isOver = true;
        winImage.alpha = 1;
        won = true;
        play = transform.FindChild("WinImage").transform.FindChild("PlayAgainButton").GetComponent<Button>();
        menu = transform.FindChild("WinImage").transform.FindChild("GoMenuButton").GetComponent<Button>();
        Time.timeScale = 0;
        Highlight(play);
        selected = play;
        if (Input.GetKeyDown("up"))
        {
           
                Highlight(play);
                Grey(menu);
                selected = play;
            
        }
        else if (Input.GetKeyDown("down"))
        {
            
                Highlight(menu);
                Grey(play);
                selected = menu;
            
        }
        if (Input.GetKeyDown("return") || Input.GetButtonDown("Fire2"))
        {
            selected.onClick.Invoke();

        }
    }

    void LoseScreen()
    {

        if (!won)
        {
            soundtrack.Stop();
            loseAudio.Play();
            isOver = true;
            loseImage.alpha = 1;
            play = transform.FindChild("LoseImage").transform.FindChild("TryAgainButton").GetComponent<Button>();
            menu = transform.FindChild("LoseImage").transform.FindChild("GoMenuButton").GetComponent<Button>();
            Highlight(play);
            selected = play;
            if (Input.GetKeyDown("up"))
            {
                Debug.Log("up");
                    Highlight(play);
                    Grey(menu);
                    selected = play;
                
            }
            else if (Input.GetKeyDown("down"))
            {
                Debug.Log("down");
                    Highlight(menu);
                    Grey(play);
                    selected = menu;
                
            }
            if (Input.GetKeyDown("return") || Input.GetButtonDown("Fire2"))
            {
                selected.onClick.Invoke();

            }
        }

        Time.timeScale = 0;
    }

    void Grey(Button btn)
    {
        tempCol = btn.colors;
        tempCol.normalColor = gr_btn;
        btn.colors = tempCol;
    }

    void Highlight(Button btn)
    {
        tempCol = btn.colors;
        tempCol.normalColor = highlighted;
        btn.colors = tempCol;
    }
}