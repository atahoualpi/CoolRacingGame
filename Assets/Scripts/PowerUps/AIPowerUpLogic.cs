using UnityEngine;
using System.Collections;

public class AIPowerUpLogic : MonoBehaviour {
    public PickUpOwnerScript PUOS;
    public SwapManager SM;
    private System.String pickup;
    public bool locking;
    public bool DroppingBanana;

    // Use this for initialization
    void Awake () {
        //PUOS = GetComponent<PickUpOwnerScript>();
        locking = false;
        DroppingBanana = false;

    }
	
	// Update is called once per frame
	void Update () {
	    if(PUOS.ownedPickUp == null) {
            return;
        }
        pickup = PUOS.ownedPickUp;
        if(pickup == "Banana" || pickup == "Banana(Clone)" && !DroppingBanana) {
            DroppingBanana = true;
            StartCoroutine(BananaDrop(Random.Range(3f,7f)));
        }

        if (pickup == "Boost" || pickup == "Boost(Clone)") {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.forward, 20);
            foreach (RaycastHit h in hits) {
                if (h.transform.CompareTag("Wall")) {
                    return;
                }
            }
            Debug.Log(this.name + " IS BOOSTING");

            PUOS.oppUsePowerUp();
        }
        if(pickup == "Thief" || pickup == "Thief(Clone)") {
            if(SM.targetCar != null && !locking) {
                Debug.Log(this.name + "IS LOCKING ON " + SM.targetCar.name);
                locking = true;
                StartCoroutine( LockNSwap(SM.targetCar));
            }
        }
    }

    IEnumerator BananaDrop(float t) {
        yield return new WaitForSeconds(t);
        if(pickup == "Banana" || pickup == "Banana(Clone)") {
            PUOS.oppUsePowerUp();
            Debug.Log(this.name + " DROPPED BANANA");

        }
        DroppingBanana = false;

    }

    IEnumerator LockNSwap(GameObject target) {
        yield return new WaitForSeconds(1f);
        if(SM.targetCar != null && SM.targetCar == target) {
            PUOS.oppUsePowerUp();
        }
        locking = false;
    }
}
