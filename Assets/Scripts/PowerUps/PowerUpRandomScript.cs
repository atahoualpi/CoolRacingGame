using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class PowerUpRandomScript : MonoBehaviour {

    string[] powerUpNames;
    GameObject powerUp;
    DirectoryInfo dir;

    Mesh mesh;

    public int numEachPowerUp;
    ShuffleBag shuffleBag;
    string chosenOne;
    int amount = 0;
    string nothingPath;

    Vector3[] puPlaceArray;
    Vector3 puPlace;

    void Awake()
    {
        // put all power ups + nothing in an array
        dir = new DirectoryInfo("Assets/Resources/PowerUps");
        FileInfo[] info = dir.GetFiles("*.prefab");
        powerUpNames = info.Select(f => f.FullName).ToArray();
        foreach (string pu in powerUpNames)
        {
            if (pu.Split('\\').Last().Split('.')[0] == "Nothing")
            {
                nothingPath = pu;
            }
        }
        //if(this.name == "line(Clone)")
        //{
        //mesh = GetComponent<MeshFilter>().mesh;
        //}
        //else
        //{
        //    mesh = transform.FindChild("Cube").transform.GetComponent<MeshFilter>().mesh;

        //}
        if (this.name == "line(Clone)")
        {
            //puPlaceArray = new float[3] { 0, (mesh.bounds.size.x / 4) - 1f, -(mesh.bounds.size.x / 4) + 1f };
            puPlaceArray = new Vector3[3] { new Vector3(-1.5f, 0.3f, 0), new Vector3(0, 0.3f, 0), new Vector3(1.5f, 0.3f, 0) };
        }

        else if (this.name == "turnLeft(Clone)")
        {
            puPlaceArray = new Vector3[3] { new Vector3(-2f, 0.3f, -2f), new Vector3(-1f, 0.3f, -1f), new Vector3(0, 0.3f, 0) };
        }
        else if (this.name == "turnRight(Clone)")
        {
            puPlaceArray = new Vector3[3] { new Vector3(0, 0.3f, 0), new Vector3(1f, 0.3f, -1f), new Vector3(2f, 0.3f, -2f) };
        }


    }
    // Use this for initialization
    void Start () {

        // choose a power up from the shuffle bag
        numEachPowerUp = 2;
        shuffleBag = new ShuffleBag(powerUpNames.Length);
        ChoosePowerUp();
        InstantiatePowerUp();
    }

    // Update is called once per frame
    void Update () {
        if(powerUp == null)
        {
            //StartCoroutine(Wait10());
        }

    }

    void ChoosePowerUp()
    {
        foreach (string pu in powerUpNames)
        {
            amount = 3 + Array.IndexOf(powerUpNames, pu) * numEachPowerUp; // starting with 3 each, variate frequency of power up appearance
            if (amount > 3 + Array.IndexOf(powerUpNames, nothingPath) * numEachPowerUp)
            {
                amount = (3 + Array.IndexOf(powerUpNames, nothingPath) * numEachPowerUp) - 1;
            }

            shuffleBag.Add(pu, amount);
        }

        chosenOne = shuffleBag.Next().Split('\\').Last().Split('.')[0];
    }

    void InstantiatePowerUp()
    {
        // choose a placement
        puPlace = puPlaceArray[UnityEngine.Random.Range(0, puPlaceArray.Length)];

        // Instantiate the power up
        powerUp = Instantiate(Resources.Load("PowerUps/" + chosenOne)) as GameObject;
        powerUp.transform.parent = this.gameObject.transform;
        powerUp.transform.localPosition = puPlace;
    }

    IEnumerator Wait10()
    {
        yield return new WaitForSeconds(10f);
        ChoosePowerUp();
        Debug.Log(chosenOne);
        InstantiatePowerUp();
    }
}
