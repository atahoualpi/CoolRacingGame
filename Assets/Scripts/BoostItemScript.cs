using UnityEngine;
using System.Collections;

public class BoostItemScript : MonoBehaviour {

    GameObject Racer;

    // Use this for initialization

    void Awake()
    {
        Racer = GameObject.Find("Racer");
    }


    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {

        if (collision.transform.tag == "ItemCollider")
        {
            Debug.Log("boost");
            Destroy(this.gameObject);
            Racer.transform.GetComponent<RacerScript>().activateBoost();
        }
    }
}
