using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PUChooseScript : MonoBehaviour {
    Image puImage;
    GameObject fruit;

    // Use this for initialization
    void Start() {
        puImage = GameObject.Find("Canvas").GetComponent<UIStuffScript>().PUimage;
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider other) {
        //if (other.tag == "ActualVehicle" || other.tag == "Opponent") {
        if (other.tag == "ActualVehicle" || other.tag == "Opponent") {
            other.GetComponent<PickUpOwnerScript>().pickAudio.Play();

            foreach (Transform child in other.transform) {
                if (child.name == "backBanana(Clone)") {
                    Destroy(child.gameObject);
                }
            }
            if (other.transform.FindChild("Swap Colliders").gameObject.activeSelf && other.transform.FindChild("Swap Colliders").GetComponent<SwapManager>().targetCar != null) {
                other.transform.FindChild("Swap Colliders").GetComponent<SwapManager>().targetCar.transform.FindChild("SwapIndicator").gameObject.SetActive(false);
            }
            other.transform.FindChild("Swap Colliders").gameObject.SetActive(false);

            other.GetComponent<PickUpOwnerScript>().ownedPickUp = this.name;

            if (this.name == "Thief" || this.name == "Thief(Clone)") {
                other.transform.FindChild("Swap Colliders").gameObject.SetActive(true);
            }
            if (other.tag == "ActualVehicle") {
                puImage.sprite = Resources.Load<Sprite>("Images/" + this.name);
                Color temp = puImage.color;
                temp.a = 1f;
                puImage.color = temp;
            }


            if (fruit != null && this.name == "Banana" || this.name == "Banana(Clone)") {
                fruit = Instantiate(Resources.Load("Prefabs/backBanana")) as GameObject;
                fruit.transform.parent = other.transform;
                fruit.transform.localPosition = new Vector3(0, fruit.transform.position.z - 0.5f, fruit.transform.position.z - 3f);
            }

            transform.parent.GetComponent<PowerUpRandomScript>().ReInstantiate();
            Destroy(gameObject);
        }
    }
}
