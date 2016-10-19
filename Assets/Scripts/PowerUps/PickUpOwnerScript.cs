using UnityEngine;
using System.Collections;
using System;

public class PickUpOwnerScript : MonoBehaviour {

    public String ownedPickUp;
    GameObject fruit;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            if(ownedPickUp == "Banana" || ownedPickUp == "Banana(Clone)")
            {
                Banana();
            }
        }
	
	}

    void Banana()
    {
        fruit = Instantiate(Resources.Load("Prefabs/DolBananapeel")) as GameObject;
        fruit.transform.position = transform.position - transform.forward;
        fruit.transform.position = new Vector3(fruit.transform.position.x, 0, fruit.transform.position.z);
    }
}
