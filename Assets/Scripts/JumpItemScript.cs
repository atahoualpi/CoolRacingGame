using UnityEngine;
using System.Collections;

public class JumpItemScript : MonoBehaviour {
    GameObject Racer;

    // Use this for initialization

    void Awake(){
        Racer = GameObject.Find("Racer");
    }


    // Update is called once per frame
    void Update () {
	
	}
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("thing");
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "ItemCollider")
        {
            Destroy(this.gameObject);
            Racer.transform.GetComponent<RacerScript>().addJump();
        }
    }
}
