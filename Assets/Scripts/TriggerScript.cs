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
            if (other.GetComponent<WayPointsScript>().targetWayPoint.position == this.transform.parent.transform.position) {

            }
        }
        if (other.tag == "ActualVehicle") {

        }
        
        
    }
}
