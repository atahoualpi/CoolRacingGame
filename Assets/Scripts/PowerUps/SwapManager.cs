using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SwapManager : MonoBehaviour {
    List<GameObject> targets;
    List<GameObject> cars;
    public GameObject thisCar;
    public GameObject Racers;
    public GameObject targetCar;
    // Use this for initialization

    void Awake() {
        targets = new List<GameObject>();
        targetCar = new GameObject();
    }

    void Start () {
        //Time.timeScale = 0.1f;
	}

    // Update is called once per frame
    void FixedUpdate() {

        cars = allCars();
        //Debug.Log("UPDATE");

        foreach (GameObject c in targets) {
            if (c != null) {
                if (closestCar() == c) {
                    c.transform.FindChild("SwapIndicator").gameObject.SetActive(true);
                    targetCar = c;
                }
                else {
                    c.transform.FindChild("SwapIndicator").gameObject.SetActive(false);
                }
            }
        }
        if (targets.Count == 0) {
            targetCar = null;
        }
        foreach(GameObject c in cars) {
            if (c != null) {
                if (!targets.Contains(c) && c != transform.parent) {
                //Debug.Log(c.name);
                c.transform.FindChild("SwapIndicator").gameObject.SetActive(false);
                }
            }

        }
        Debug.Log(targets.Count);
        //Debug.Log(targets.Count);
        targets.Clear();

    }

    //void LateUpdate() {
    //    targets.Clear();
    //}



    public void addTarget(GameObject car) {
        //Debug.Log("ADD TARGER");
        if (car != transform.parent.gameObject) {
            if (!targets.Contains(car)) {
                targets.Add(car);
            }
        }
    }

    GameObject closestCar() {
        float shortest = 100000f;
        GameObject potCar = null;     
        foreach(GameObject c in targets) {
            if (c != null) {
                float d = Vector3.Distance(thisCar.transform.position, c.transform.position);
                if (d <= shortest) {
                    potCar = c;
                    shortest = d;
                }
            }
            //Debug.Log(c.name);
        }
        return potCar;
    }

    List<GameObject> allCars() {
        List<GameObject> cars = new List<GameObject>();
        foreach(Transform child in Racers.transform) {
            cars.Add(child.gameObject);
        }
        return cars;
    }
}
