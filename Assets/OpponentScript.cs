using UnityEngine;
using System.Collections;

public class OpponentScript : MonoBehaviour {

    private NavMeshAgent nav;
    private Transform goal;

    // Use this for initialization
    void Awake () {
        nav = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("GoalPoint").transform;
        Drive(goal.position);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void Drive(Vector3 goalPos)
    {
        nav.SetDestination(goalPos);
    }
}
