using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwapManager : MonoBehaviour {
    List<GameObject> targets;
    public GameObject thisCar;
    // Use this for initialization

    void Awake() {
        targets = new List<GameObject>();
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject c in targets) {
            if(closestCar() == c) {
                c.transform.FindChild("SwapIndicator").gameObject.SetActive(true);
            }
            else {
                c.transform.FindChild("SwapIndicator").gameObject.SetActive(false);
            }
        }
        //Debug.Log(targets.Count);
	}

    public void addTarget(GameObject car) {
        if(car != transform.parent.gameObject) {
            if (!targets.Contains(car)) {
                targets.Add(car);
            }
        }
    }

    GameObject closestCar() {
        float shortest = Vector3.Distance(thisCar.transform.position, targets[0].transform.position);
        GameObject potCar = targets[0];     
        foreach(GameObject c in targets) {
            float d = Vector3.Distance(thisCar.transform.position, c.transform.position);
            if (d < shortest){
                potCar = c;
                shortest = d;
            }
            //Debug.Log(c.name);
        }
        return potCar;
    }
}
