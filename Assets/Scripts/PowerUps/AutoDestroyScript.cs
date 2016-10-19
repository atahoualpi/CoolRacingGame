using UnityEngine;
using System.Collections;

public class AutoDestroyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(AutoDestroy());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
