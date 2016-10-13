using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {
    GameObject Racer;
    Text DistanceText;
    Text GameOver;
	// Use this for initialization
	void Awake () {
        Racer = GameObject.Find("Racer");
        DistanceText = transform.GetComponent<Text>();
        GameOver = transform.FindChild("Game Over").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update () {
        DistanceText.text = "Distance: " + ((int)Racer.transform.position.z) + "m \nJumps: " + Racer.GetComponent<RacerScript>().getJumps(); ;
        if(Racer.GetComponent<RacerScript>().isDead)
        {
            GameOver.text = "Game Over\nDistance Traveled: " + ((int)Racer.transform.position.z) + "\nPush space/X to start again";
        }
    }
}
