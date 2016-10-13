using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

    GameObject Racer;

    // Use this for initialization
    void Awake () {
        Racer = GameObject.Find("Racer");

    }

    // Update is called once per frame
    void Update () {
        if (Racer.transform.position.z > this.transform.position.z + 500)
        {
            Destroy(this.gameObject);
        }
	}
}
