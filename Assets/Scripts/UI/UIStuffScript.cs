using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update () {

    }

}
