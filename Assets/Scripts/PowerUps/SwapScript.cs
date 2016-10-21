using UnityEngine;
using System.Collections;

public class SwapScript : MonoBehaviour {
    public SwapManager SM;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other){
        if(other.CompareTag("ActualVehicle") || other.CompareTag("Opponent")) {
            SM.addTarget(other.gameObject);
        }
    }


}
