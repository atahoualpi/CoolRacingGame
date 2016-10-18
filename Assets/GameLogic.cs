using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

    public List<GameObject> vehicles;

    float t;

    void Awake() {
        t = 0;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
            if(t > 20) {
                killLastCar();
                t = 0;
            }
	}

    void killLastCar() {
        string DestroyedCar = "";
        int lastVal = vehicles[0].GetComponent<WayPointsScript>().currentWayPoint;
        foreach (GameObject c in vehicles) {
            if (lastVal > c.GetComponent<WayPointsScript>().currentWayPoint) {
                lastVal = c.GetComponent<WayPointsScript>().currentWayPoint;
            }
        }
        List<GameObject> potCars = new List<GameObject>();
        List<int> indices = new List<int>();
        for (int i = 0; i < 4; i++) {
            if (vehicles[i].GetComponent<WayPointsScript>().currentWayPoint == lastVal) {
                potCars.Add(vehicles[i]);
                indices.Add(i);
            }
        }
        if(potCars.Count == 1) {
            //DestroyedCar = po
            Destroy(potCars[0].gameObject);
        }
        else {
            SortedDictionary<float, GameObject> dic = new SortedDictionary<float, GameObject>();
            foreach(GameObject c in potCars) {
                dic.Add(c.GetComponent<WayPointsScript>().getDistToNextWP(), c);
            }

        }

    }
}
