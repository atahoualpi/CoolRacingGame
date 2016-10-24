using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuSript : MonoBehaviour {
    private Button lapMode;
    private Button timeMode;

    Color highlighted;
    Color greyed;
    ColorBlock tempCol;

    public LevelChangeScript lvl;

    // Use this for initialization
    void Start () {
        lapMode = transform.FindChild("LapModeButton").GetComponent<Button>();
        timeMode = transform.FindChild("TimeModeButton").GetComponent<Button>();

        highlighted = new Color(0.7f, 0.96f, 0.62f, 1f);
        greyed = new Color(0.18f, 0.24f, 0.15f, 1f);

        //lapMode.Select();
        ChooseBtn(1);
        lvl.chooseScene = 1;

    }

    // Update is called once per frame
    void Update () {
        

    }

    public void ChooseBtn(int index)
    {
        Button btn1;
        Button btn2;
        if (index == 1)
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
}
