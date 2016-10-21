using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PickUpOwnerScript : MonoBehaviour {

    public String ownedPickUp;
    public bool steal;
    public bool isDropped;
    GameObject fruit;
    GameObject currOpp;
    GameObject backBanana;
    Image puImage;

    // Use this for initialization
    void Start () {
        currOpp = null;
        isDropped = false;
        puImage = GameObject.Find("Canvas").GetComponent<UIStuffScript>().PUimage;
    }

    IEnumerator DestroyBackBanana()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("back: " + backBanana.transform.position);

        Destroy(backBanana);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("space"))
        {
            if (ownedPickUp == "Banana" || ownedPickUp == "Banana(Clone)")
            {
                backBanana = this.transform.FindChild("backBanana(Clone)").gameObject;
                backBanana.GetComponent<Animator>().SetTrigger("drop");
                StartCoroutine(DestroyBackBanana());
                Banana();
            }
            if (ownedPickUp == "Thief" || ownedPickUp == "Thief(Clone)")
            {
                Thief();
            }
            if (ownedPickUp == "Boost" || ownedPickUp == "Boost(Clone)")
            {
                GetComponent<RacerScript>().Boost();
            }
            Color temp = puImage.color;
            temp.a = 0;
            puImage.color = temp;
        }
	
	}

    void Banana()
    {
        isDropped = true; 
        fruit = Instantiate(Resources.Load("Prefabs/DolBananapeel")) as GameObject;
        fruit.transform.position = transform.position - transform.forward;
        Debug.Log("new: " + fruit.transform.position);
        fruit.transform.position = new Vector3(fruit.transform.position.x, 0, fruit.transform.position.z);
        ownedPickUp = null;
    }

    void Thief()
    {
        if (GetComponent<StealPosScript>().inTrigger)
        {
            currOpp = GetComponent<StealPosScript>().curOpp;
            Debug.Log(currOpp.name);
            Vector3 tempVec = transform.position;
            transform.position = new Vector3(currOpp.transform.position.x, currOpp.transform.position.y, currOpp.transform.position.z);
            currOpp.transform.position = new Vector3(tempVec.x, tempVec.y, tempVec.z);
            int tempInt = currOpp.GetComponent<WayPointsScript>().currentWayPoint;
            currOpp.GetComponent<WayPointsScript>().currentWayPoint = GetComponent<WayPointsScript>().currentWayPoint;
            GetComponent<WayPointsScript>().currentWayPoint = tempInt;
            Vector3 tempTr = currOpp.GetComponent<WayPointsScript>().targetWayPoint;
            currOpp.GetComponent<WayPointsScript>().targetWayPoint = GetComponent<WayPointsScript>().targetWayPoint;
            GetComponent<WayPointsScript>().targetWayPoint = tempTr;
            GetComponent<StealPosScript>().inTrigger = false;
            currOpp = null;
        }
        ownedPickUp = null;
    }

    //void Boost()
    //{
    //    // a smart way to accelerate goes here :)
          // nope it went to racerscript -> ask there for help 
    //}
}
