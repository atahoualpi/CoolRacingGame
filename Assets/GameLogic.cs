using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameLogic : MonoBehaviour {

    private List<GameObject> vehicles;
    public GameObject Racers;

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
            if(t > 5) {
                killLastCar();
                t = 0;
            }
	}

    void killLastCar() {

        vehicles = new List<GameObject>();
        foreach(Transform child in Racers.transform) {
            vehicles.Add(child.gameObject);
        }

        string DestroyedCar = "";
        int lastVal = vehicles[0].GetComponent<WayPointsScript>().currentWayPoint;
        foreach (GameObject c in vehicles) {
            if (lastVal > c.GetComponent<WayPointsScript>().currentWayPoint) {
                lastVal = c.GetComponent<WayPointsScript>().currentWayPoint;
            }
        }

        List<GameObject> potCars = new List<GameObject>();
        List<int> indices = new List<int>();
        for (int i = 0; i < vehicles.Count; i++) {
            if (vehicles[i].GetComponent<WayPointsScript>().currentWayPoint == lastVal) {
                potCars.Add(vehicles[i]);
                indices.Add(i);
            }
        }
        if(potCars.Count == 1) {
            //DestroyedCar = po
            tryDestroy(potCars[0].gameObject);
            //Debug.Log("ONLY ONE");
        }
        else {
            //Debug.Log("MORE THAN ONE");

            SortedDictionary<float, GameObject> dic = new SortedDictionary<float, GameObject>();
            foreach(GameObject c in potCars) {
                dic.Add(c.GetComponent<WayPointsScript>().getDistToNextWP(), c);
            }
            tryDestroy(dic.Last().Value);
        }

    }
    void tryDestroy(GameObject car) {
        if (car.tag == "Opponent") {
            Destroy(car);
        }
        else if (car.tag == "ActualVehicle") {
            //Debug.Log("YOU LOSE");
        }

    }
}
