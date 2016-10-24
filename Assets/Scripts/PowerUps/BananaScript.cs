using UnityEngine;
using System.Collections;

public class BananaScript : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {


        if (other.tag == "ActualVehicle") {
            if (other.GetComponent<RacerScript>().spinning) {
                other.transform.FindChild("GameObject").GetComponent<Animator>().enabled = true;

                other.transform.FindChild("GameObject").GetComponent<Animator>().SetTrigger("bananaSpin");
                other.GetComponent<RacerScript>().hitBanana();
            }
            Destroy(gameObject);

        }
        else if (other.tag == "Opponent") {
            if (other.GetComponent<OpponentMovement>().spinning) {
                other.transform.FindChild("GameObject").GetComponent<Animator>().enabled = true;

                other.transform.FindChild("GameObject").GetComponent<Animator>().SetTrigger("bananaSpin");
                other.GetComponent<OpponentMovement>().hitBanana();
            }
            Destroy(gameObject);

        }

    }
}
