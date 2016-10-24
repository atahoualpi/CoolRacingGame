using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwapScript : MonoBehaviour {
    public SwapManager SM;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerStay(Collider other) {
        if (other.CompareTag("ActualVehicle") || other.CompareTag("Opponent")) {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, other.transform.position - transform.position, Vector3.Distance(transform.position, other.transform.position));
            foreach (RaycastHit h in hits) {
                if (h.transform.CompareTag("Wall")) {

                    return;
                }
            }
            SM.addTarget(other.gameObject);
        }
    }


}
