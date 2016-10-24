using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PickUpOwnerScript : MonoBehaviour {

    public String ownedPickUp;
    public bool steal;
    public bool isDropped;
    GameObject fruit;
    public GameObject currOpp;
    GameObject backBanana;
    Image puImage;

    public bool isPlayer;
    private bool usePowerup = false;


    public SwapManager SM;

    public AudioSource boostAudio;
    public AudioSource bananaAudio;
    public AudioSource swapAudio;
    public AudioSource pickAudio;
    public AudioSource spinAudio;


    // Use this for initialization
    void Start() {
        currOpp = null;
        isDropped = false;
        if(isPlayer)
            puImage = GameObject.Find("Canvas").GetComponent<UIStuffScript>().PUimage;
    }

    IEnumerator DestroyBackBanana() {
        yield return new WaitForSeconds(0.5f);

        Destroy(backBanana);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("space") || (Input.GetButtonDown("Fire2")) && this.tag == "ActualVehicle") || usePowerup)
        {
            usePowerup = false;
            if (ownedPickUp == "Banana" || ownedPickUp == "Banana(Clone)")
            {
                backBanana = this.transform.FindChild("backBanana(Clone)").gameObject;
                backBanana.GetComponent<Animator>().SetTrigger("drop");
                StartCoroutine(DestroyBackBanana());
                Banana();
            }
            if (ownedPickUp == "Thief" || ownedPickUp == "Thief(Clone)")
            {
                transform.FindChild("Swap Colliders").gameObject.SetActive(false);
                Thief();
            }

            if (ownedPickUp == "Boost" || ownedPickUp == "Boost(Clone)")
            {
                boostAudio.Play();

                if (isPlayer)
                    GetComponent<RacerScript>().Boost();
                else
                    GetComponent<OpponentMovement>().Boost();

                ownedPickUp = null;

            }
            if (isPlayer)
            {
                Color temp = puImage.color;
                temp.a = 0;
                puImage.color = temp;
            }
        }
    }

    void Banana()
    {
        bananaAudio.Play();

        isDropped = true; 

        fruit = Instantiate(Resources.Load("Prefabs/DolBananapeel")) as GameObject;
        fruit.transform.position = transform.position - transform.forward;
        fruit.transform.position = new Vector3(fruit.transform.position.x, 0, fruit.transform.position.z);
        ownedPickUp = null;
    }

    void Thief() {
        //if (GetComponent<StealPosScript>().inTrigger)
        //{
        //    currOpp = GetComponent<StealPosScript>().curOpp;
        //    Debug.Log(currOpp.name);
        //    Vector3 tempVec = transform.position;
        //    transform.position = new Vector3(currOpp.transform.position.x, currOpp.transform.position.y, currOpp.transform.position.z);
        //    currOpp.transform.position = new Vector3(tempVec.x, tempVec.y, tempVec.z);
        //    int tempInt = currOpp.GetComponent<WayPointsScript>().currentWayPoint;
        //    currOpp.GetComponent<WayPointsScript>().currentWayPoint = GetComponent<WayPointsScript>().currentWayPoint;
        //    GetComponent<WayPointsScript>().currentWayPoint = tempInt;
        //    Vector3 tempTr = currOpp.GetComponent<WayPointsScript>().targetWayPoint;
        //    currOpp.GetComponent<WayPointsScript>().targetWayPoint = GetComponent<WayPointsScript>().targetWayPoint;
        //    GetComponent<WayPointsScript>().targetWayPoint = tempTr;
        //    GetComponent<StealPosScript>().inTrigger = false;
        //    currOpp = null;


        //}





        if (SM.targetCar != null) {
            currOpp = SM.targetCar;

            Vector3 tempVec = transform.position;
            transform.position = currOpp.transform.position;
            currOpp.transform.position = tempVec;

            WayPointsScript thisCarWPS = GetComponent<WayPointsScript>();
            WayPointsScript oppCarWPS = currOpp.GetComponent<WayPointsScript>();

            int tempInt = oppCarWPS.currentWayPoint;
            oppCarWPS.currentWayPoint = thisCarWPS.currentWayPoint;
            thisCarWPS.currentWayPoint = tempInt;

            oppCarWPS.currentWayPoint = convertCWP(oppCarWPS.currentWayPoint, oppCarWPS.currentLap, oppCarWPS.lapLength);
            thisCarWPS.currentWayPoint = convertCWP(thisCarWPS.currentWayPoint, thisCarWPS.currentLap, thisCarWPS.lapLength);


            tempVec = oppCarWPS.targetWayPoint;
            oppCarWPS.targetWayPoint = thisCarWPS.targetWayPoint;
            thisCarWPS.targetWayPoint = tempVec;

            swapAudio.Play();

            //GetComponent<StealPosScript>().inTrigger = false;
            currOpp.transform.FindChild("SwapIndicator").gameObject.SetActive(false);



        }
        SM.gameObject.SetActive(false);
        ownedPickUp = null;
    }

    int convertCWP(int cWP, int cL, int lapLength) {
        int baseCWP = cWP % lapLength;
        return baseCWP + ((cL - 1) * lapLength);
    }

    public void oppUsePowerUp() {
        usePowerup = true;
    }

    //void Boost()
    //{
    //    // a smart way to accelerate goes here :)
    // nope it went to racerscript -> ask there for help 
    //}
}
