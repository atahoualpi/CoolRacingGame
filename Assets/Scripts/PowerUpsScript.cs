using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class PowerUpsScript : MonoBehaviour {

    string[] powerUpNames;
    GameObject powerUp;
    DirectoryInfo dir;

    Mesh mesh;

    public int numEachPowerUp;
    ShuffleBag shuffleBag;
    string chosenOne;
    int amount = 0;
    string nothingPath;

    float[] puPlaceArray;
    float puPlace;

    string[] texturesNames;
    string currTexture;
    public int numEachTexture;
    ShuffleBag shuffleBagTexture;


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
        // put all textures in an array
        dir = new DirectoryInfo("Assets/Resources/Textures");
        FileInfo[] infoTex = dir.GetFiles("*.jpg");
        texturesNames = infoTex.Select(f => f.FullName).ToArray();

        mesh = GetComponent<MeshFilter>().mesh;
        puPlaceArray = new float[3] { 0, mesh.bounds.size.x / 4, -mesh.bounds.size.x / 4 };
    }
    // Use this for initialization
    void Start () {

        // assign a straight or turn texture
        numEachPowerUp = 2;
        shuffleBagTexture = new ShuffleBag(texturesNames.Length);
        AssignTexture();

        // choose a power up from the shuffle bag
        numEachTexture = 3;
        shuffleBag = new ShuffleBag(powerUpNames.Length);
        ChoosePowerUp();

        // choose a placement
        puPlace = puPlaceArray[UnityEngine.Random.Range(0, puPlaceArray.Length)];

        // Instantiate the power up
        powerUp = Instantiate(Resources.Load("PowerUps/" + chosenOne)) as GameObject;
        powerUp.transform.position = new Vector3(transform.position.x + puPlace, transform.position.y + 1, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        
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
        Debug.Log("Chosen PowerUp: " + chosenOne);
    }

    void AssignTexture()
    {
        foreach (string tex in texturesNames)
        {
            amount = 3 + Array.IndexOf(texturesNames, tex) * numEachTexture; // starting with 3 each, variate frequency of power up appearance
            Debug.Log(amount);

            shuffleBagTexture.Add(tex, amount);
        }
        Debug.Log(shuffleBagTexture);
        currTexture = shuffleBagTexture.Next().Split('\\').Last().Split('.')[0];
        this.GetComponent<Renderer>().material.SetTexture("_MainTex",
             (Texture)Resources.Load("Textures/" + currTexture));
    }
}
