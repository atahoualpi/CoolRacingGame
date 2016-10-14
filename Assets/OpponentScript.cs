using UnityEngine;
using System.Collections;

public class OpponentScript : MonoBehaviour {

    private NavMeshAgent nav;
    private Transform goal;

    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        goal = GameObject.FindWithTag("Route").transform;
      
        Drive(goal.position);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void Drive(Vector3 goalPos)
    {
        //nav.SetDestination(goalPos);
    }
}
