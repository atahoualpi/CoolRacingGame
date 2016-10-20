using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {


    public bool isStartPoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Opponent" || other.tag == "ActualVehicle") {
            //Debug.Log(other.GetComponent<WayPointsScript>().targetWayPoint.position);
            //Debug.Log(this.transform.parent.transform.position);
            if (other.GetComponent<WayPointsScript>().targetWayPoint.x == this.transform.parent.transform.position.x && other.GetComponent<WayPointsScript>().targetWayPoint.z == this.transform.parent.transform.position.z) {
                other.GetComponent<WayPointsScript>().EnteredTrigger();
                if(isStartPoint)
                    other.GetComponent<WayPointsScript>().newLap();

            }
        }
        if (other.tag == "ActualVehicle") {

        }
        
        
    }
}
