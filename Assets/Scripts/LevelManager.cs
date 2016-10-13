using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    GameObject Racer;
    GameObject Block1;
    GameObject Block2;
    GameObject Block3;
    GameObject[] Blocks = new GameObject[3];
    float lastZPos;


    // Use this for initialization
    void Awake() {
        Racer = GameObject.Find("Racer");
        //Block1 = Resources.Load("Block (1)") as GameObject;
        //Block2 = Resources.Load("Block (2)") as GameObject;
        //Block3 = Resources.Load("Block (3)") as GameObject;
        //Blocks[0] = Block1;
        //Blocks[1] = Block2;
        //Blocks[2] = Block3;
        for(int i = 0; i < 3; i++)
        {
            Blocks[i] = Resources.Load("Block (" + (i+1) + ")") as GameObject;
        }

        MOARBLOCKZ(300);
        lastZPos = 0;
        //string[] buttons = Input.GetJoystickNames();
        //for(int i = 0; i < buttons.Length; i++)
        //{
        //    Debug.Log(buttons[i]);
        //}
    }

    // Update is called once per frame
    void Update() {
        if ((lastZPos == 0 && Racer.transform.position.z >= lastZPos + 700) || Racer.transform.position.z >= lastZPos + 1000)
        {
            MOARBLOCKZ(600);
        }
        //Debug.Log(Input.GetAxis("Horizontal"));
        //{
        //    Debug.Log("SHOTS FIRED HURDUR");
        //}

    }
    void MOARBLOCKZ(int forward)
    {
        lastZPos = Racer.transform.position.z;
        for (int i = 0; i < 10; i++)
        {
            GameObject Block = Blocks[(int)Random.Range(0, 3)];
            Block.transform.position = new Vector3(0, 0, i * 100 + forward + (Racer.transform.position.z - (float) Racer.transform.position.z % 100.0f));
            Block.transform.Rotate(new Vector3(0, ((int)Random.Range(0, 4)) * 90, 0));
            Debug.Log(Block.transform.position.z);
            Instantiate(Block);
        }
    }
}
