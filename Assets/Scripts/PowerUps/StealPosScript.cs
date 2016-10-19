using UnityEngine;
using System.Collections;

public class StealPosScript : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "ActualVehicle")
        {
            
        }
        Destroy(gameObject);
    }
}
