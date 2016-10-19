using UnityEngine;
using System.Collections;

public class StealPosScript : MonoBehaviour
{
    public bool inTrigger = false;
    public GameObject curOpp;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Opponent")
        {
            inTrigger = true;
            curOpp = other.gameObject;        
        }
    }
}