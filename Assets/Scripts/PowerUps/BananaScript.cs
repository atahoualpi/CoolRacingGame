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
        other.transform.FindChild("GameObject").GetComponent<Animator>().SetTrigger("bananaSpin");
        Destroy(gameObject);
        if (other.tag == "ActualVehicle")
        {
            other.GetComponent<RacerScript>().hitBanana();
        }
    }
}
