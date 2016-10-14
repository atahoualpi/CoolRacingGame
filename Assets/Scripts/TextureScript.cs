using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class TextureScript : MonoBehaviour
{

    DirectoryInfo dir;

    Mesh mesh;

    int amount = 0;

    string[] texturesNames;
    string currTexture;
    public int numEachTexture;
    ShuffleBag shuffleBagTexture;


    void Awake()
    {
        // put all textures in an array
        dir = new DirectoryInfo("Assets/Resources/Textures");
        FileInfo[] infoTex = dir.GetFiles("*.jpg");
        texturesNames = infoTex.Select(f => f.FullName).ToArray();

        mesh = GetComponent<MeshFilter>().mesh;
    }
    // Use this for initialization
    void Start()
    {

        // assign a straight or turn texture
        numEachTexture = 3;
        shuffleBagTexture = new ShuffleBag(texturesNames.Length);
        AssignTexture();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AssignTexture()
    {
        foreach (string tex in texturesNames)
        {
            amount = 3 + Array.IndexOf(texturesNames, tex) * numEachTexture; // starting with 3 each, variate frequency of power up appearance

            shuffleBagTexture.Add(tex, amount);
        }
        currTexture = shuffleBagTexture.Next().Split('\\').Last().Split('.')[0];
        this.GetComponent<Renderer>().material.SetTexture("_MainTex",
             (Texture)Resources.Load("Textures/" + currTexture));
    }
}
