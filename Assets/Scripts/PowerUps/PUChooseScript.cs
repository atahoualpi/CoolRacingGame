using UnityEngine;
using System.Collections;

public class PUChooseScript : MonoBehaviour {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ActualVehicle")
        {
            other.GetComponent<PickUpOwnerScript>().ownedPickUp = this.name;
            Destroy(gameObject);
        }
    }
}
