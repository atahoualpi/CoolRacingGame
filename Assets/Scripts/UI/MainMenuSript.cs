using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuSript : MonoBehaviour {
    private Text titleText;
    float r, g, b;
	// Use this for initialization
	void Start () {
        titleText = transform.FindChild("TitleText").GetComponent<Text>();
        r = 191/255f;
        g = 100 / 255f;
        b = 100 / 255f;
        titleText.color = new Color(r, g, b, 1);

    }

    // Update is called once per frame
    void Update () {
        //if (r == 0.749f)
        //{
        //    b += 1 / 255f;
        //    titleText.color = new Color(r, g, b, 1);


        //    if (b == 191 / 255f)
        //    {
        //        r -= 1 / 255f;
        //        titleText.color = new Color(r, g, b, 1);


        //        if (r == 100 / 255f)
        //        {
        //            g += 1 / 255f;
        //            titleText.color = new Color(r, g, b, 1);


        //            if (g == 191 / 255f)
        //            {
        //                b -= 1 / 255f;
        //                titleText.color = new Color(r, g, b, 1);

        //                if (b == 100 / 255f)
        //                {
        //                    r += 1 / 255f;
        //                    titleText.color = new Color(r, g, b, 1);

        //                    if (r == 192 / 255f)
        //                    {
        //                        g -= 1 / 255f;
        //                        titleText.color = new Color(r, g, b, 1);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

    }
}
