using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MainMenuSript : MonoBehaviour {
    private Button lapMode;
    private Button timeMode;
    private Button play;
    private Button quit;
    private Button mode;
    private Button selected;
    public bool isTime;
    private bool edged;
    String dir;

    Color highlighted;
    Color greyed;
    Color gr_btn;
    ColorBlock tempCol;

    public LevelChangeScript lvl;
    private int buttonSelector;

    // Use this for initialization
    void Start () {

        edged = false;
        Cursor.visible = false;
        isTime = false;
        buttonSelector = 1;
        lapMode = transform.FindChild("LapModeButton").GetComponent<Button>();
        timeMode = transform.FindChild("TimeModeButton").GetComponent<Button>();
        play = transform.FindChild("PlayButton").GetComponent<Button>();
        quit = transform.FindChild("QuitButton").GetComponent<Button>();
        mode = transform.FindChild("ChangeModeButton").GetComponent<Button>();

        highlighted = new Color(0.7f, 0.96f, 0.62f, 1f);
        greyed = new Color(0.18f, 0.24f, 0.15f, 1f);
        gr_btn = new Color(0.24f, 0.42f, 0.18f, 1f);

        //lapMode.Select();
        Highlight(play);
        selected = play;
        ChooseMode();
        lvl.chooseScene = 1;

    }

    // Update is called once per frame
    void Update () {


        if(Input.GetAxis("Horizontal") < -0.1) {
            ChooseMode();
        }
        else if (Input.GetAxis("Horizontal") > 0.1) {
            ChooseMode();
        }

        GetLeftDir();


        if (Input.GetKeyDown("up"))
        {                                            
            if (buttonSelector != 1)
            {
                buttonSelector -= 1;
                ChooseBtn(buttonSelector);
            }
        }
        //if(JSM.getLeftDir((float)Input.GetAxis("LeftH"), (float)Input.GetAxis("LeftV")) != "") {
        //    Debug.Log(JSM.getLeftDir((float)Input.GetAxis("LeftH"), (float)Input.GetAxis("LeftV")));
        //}
        //Debug.Log(JSM.getLeftDir());
        if (Input.GetKeyDown("down"))
        {
            if (buttonSelector != 3)
            {
                buttonSelector += 1;
                ChooseBtn(buttonSelector);
            }
        }
        if (Input.GetKeyDown("return") || Input.GetButtonDown("Fire2"))
        {
            selected.onClick.Invoke();

        }

    }

    void selectUp() {
        if (buttonSelector != 1) {
            buttonSelector -= 1;
            ChooseBtn(buttonSelector);
        }
    }

    void selectDown() {
        if (buttonSelector != 3) {
            buttonSelector += 1;
            ChooseBtn(buttonSelector);
        }
    }

    public void ChooseBtn(int selector)
    {
        switch (selector)
        {
            case 1:
                selected = play;
                Highlight(play);
                Grey(quit);
                Grey(mode);
                break;
            case 2:
                selected = mode;
                Highlight(mode);
                Grey(quit);
                Grey(play);
                break;
            case 3:
                selected = quit;
                Highlight(quit);
                Grey(play);
                Grey(mode);
                break;
        }
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

    public void Change()
    {
        isTime = !isTime;
        ChooseMode();
    }

    public void ChooseMode()
    {
        Button btn1;
        Button btn2;

        if (!isTime)
        {
            btn1 = lapMode;
            btn2 = timeMode;
            lvl.chooseScene = 1;
        }
        else
        {
            btn1 = timeMode;
            btn2 = lapMode;
            lvl.chooseScene = 2;
        }
        tempCol = btn1.colors;
        tempCol.normalColor = highlighted;
        btn1.colors = tempCol;

        tempCol = btn2.colors;
        tempCol.normalColor = greyed;
        btn2.colors = tempCol;
    }

    void GetLeftDir() {
        float x = Input.GetAxis("LeftH");
        float y = Input.GetAxis("LeftV");
        float magx = Mathf.Abs(x);
        float magy = Mathf.Abs(y);

        if (Mathf.Sqrt(Mathf.Pow(magx, 2) + Mathf.Pow(magy, 2)) > 0.8) {
            if (!edged) {
                edged = true;
                //if (x >= 0 && magx >= magy) {
                //}
                //else if (x <= 0 && magx >= magy) {
                //}
                if (y >= 0 && magy >= magx) {
                    //Debug.Log("up");
                    selectUp();

                }
                else if (y <= 0 && magy >= magx) {
                    //Debug.Log("down");
                    selectDown();

                }
            }
        }
        else {
            //Debug.Log("CENTERED");
            edged = false;
        }
    }

    //String getLeftDir(float x, float y) {

    //    float magx = Mathf.Abs(x);
    //    float magy = Mathf.Abs(y);

    //    if (Mathf.Sqrt(Mathf.Pow(magx, 2) + Mathf.Pow(magy, 2)) > 0.8) {
    //        if (!edged) {
    //            edged = true;
    //            if (x >= 0 && magx >= magy) {
    //                Debug.Log("RIGHT");
    //                return "right";
    //            }
    //            else if (x <= 0 && magx >= magy) {
    //                Debug.Log("left");

    //                return "left";

    //            }
    //            else if (y >= 0 && magy >= magx) {
    //                //Debug.Log("up");

    //                return "up";

    //            }
    //            else if (y <= 0 && magy >= magx) {
    //                Debug.Log("down");

    //                return "down";

    //            }
    //        }
    //        return "";

    //    }
    //    else {
    //        //Debug.Log("CENTERED");
    //        edged = false;
    //        return "";
    //    }
    //}
}
