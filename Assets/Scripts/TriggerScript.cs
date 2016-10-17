using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Opponent") {
            Debug.Log(other.GetComponent<WayPointsScript>().targetWayPoint.position);
            Debug.Log(this.transform.parent.transform.position);
            if (other.GetComponent<WayPointsScript>().targetWayPoint.position.x == this.transform.parent.transform.position.x && other.GetComponent<WayPointsScript>().targetWayPoint.position.z == this.transform.parent.transform.position.z) {

                other.GetComponent<WayPointsScript>().EnteredTrigger();
            }
        }
        if (other.tag == "ActualVehicle") {
            Debug.Log("PLAYER HIT A THING");
        }
        
        
    }
}
