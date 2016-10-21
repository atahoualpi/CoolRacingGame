using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameLogic : MonoBehaviour {

    private List<GameObject> vehicles;
    private SortedDictionary<float, String> laps;
    public GameObject Racers;
    private bool isTimeMode = true;


    public float t;

    int count = 0;



    void Awake() {
        if (isTimeMode)
            t = 20;
        else
            t = 0;

        laps = new SortedDictionary<float, String>();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isTimeMode) {
            t -= Time.deltaTime;
            if (t <= 0) {
                List<GameObject> c = rankCars();
                Destroy(c[c.Count - 1]);
                
                //killLastCar();
                //t = 0;
            }
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
            Debug.Log("YOU LOSE");
            stopGame();
        }

    }

    public void addLap(float t, String s, bool done) {
        laps.Add(t, s);

        if (done) {
            count++;
        }

        if (count == 3) {
            foreach (KeyValuePair<float, String> entry in laps) {
                Debug.Log("LAP TIME: " + entry.Key + " " + entry.Value);
            }
        }
    }

    public List<GameObject> rankCars() {
        List<GameObject> Vehicles = new List<GameObject>();
        List<GameObject> rankedCars = new List<GameObject>();
        foreach (Transform child in Racers.transform) {
            Vehicles.Add(child.gameObject);
        }

        SortedDictionary<int, List<GameObject>> dic = new SortedDictionary<int, List<GameObject>>();

        foreach (GameObject c in Vehicles) {
            int pos = c.GetComponent<WayPointsScript>().currentWayPoint;
            if (dic.ContainsKey(pos)) {
                dic[pos].Add(c);
            }
            else {
                dic.Add(pos, new List<GameObject>() { c });
            }
        }
        foreach (KeyValuePair<int, List<GameObject>> entry in dic) {
            if (entry.Value.Count == 1) {
                rankedCars.Add(entry.Value[0]);
            }
            else {
                SortedDictionary<float, GameObject> dicc = new SortedDictionary<float, GameObject>();
                List<GameObject> cccc = new List<GameObject>();
                foreach (GameObject c in entry.Value) {
                    dicc.Add(c.GetComponent<WayPointsScript>().getDistToNextWP(), c);
                }
                foreach(KeyValuePair<float, GameObject> ccc in dicc) {
                    cccc.Add(ccc.Value);
                }
                cccc.Reverse();
                rankedCars.AddRange(cccc);
            }
        }
        rankedCars.Reverse();
        return rankedCars;
    }

    public void stopGame() {
        Time.timeScale = 0;
    }

}
