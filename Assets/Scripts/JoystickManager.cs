using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class JoystickManager : MonoBehaviour {

    

    private bool edged;
    // Use this for initialization
    void Start () {
        edged = false;
    }

    // Update is called once per frame
    void Update () {
        //float x = Input.GetAxis("LeftH");
        //float y = Input.GetAxis("LeftV");
        //move(x, y, ref edgedL);
        ////x = Input.GetAxis("RightH");
        ////y = Input.GetAxis("RightV");
        ////move(x, y, ref edgedR);


    }

    //public String getLeftDir() {
    //    float x = Input.GetAxis("LeftH");
    //    float y = Input.GetAxis("LeftV");
    //    return move(x, y, ref edgedL);
    //}

    public String getLeftDir(float x, float y)
    {

        float magx = Mathf.Abs(x);
        float magy = Mathf.Abs(y);

        if (Mathf.Sqrt(Mathf.Pow(magx, 2) + Mathf.Pow(magy, 2)) > 0.8)
        {
            if (!edged) {
                edged = true;
                if (x >= 0 && magx >= magy)
                {
                    Debug.Log("RIGHT");
                    return "right";
                }
                else if (x <= 0 && magx >= magy) {
                    Debug.Log("left");

                    return "left";

                }
                else if (y >= 0 && magy >= magx) {
                    //Debug.Log("up");

                    return "up";

                }
                else if (y <= 0 && magy >= magx) {
                    Debug.Log("down");

                    return "down";

                }
            }
            return "";

        }
        else
        {
            //Debug.Log("CENTERED");
            edged = false;
            return "";
        }
    }
}
