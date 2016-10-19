using UnityEngine;
using System.Collections;

public class BananaScript : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);


            if (other.tag == "ActualVehicle") {
            if (other.GetComponent<RacerScript>().spinning) {
                other.transform.FindChild("GameObject").GetComponent<Animator>().enabled = true;

                other.transform.FindChild("GameObject").GetComponent<Animator>().SetTrigger("bananaSpin");
                other.GetComponent<RacerScript>().hitBanana();
            }
        }
    }
}
