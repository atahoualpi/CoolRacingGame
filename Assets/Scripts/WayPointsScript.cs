using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPointsScript : MonoBehaviour
{

    // put the points from unity interface
    public List<Vector3> wayPointList_list = new List<Vector3>();

    Vector3 startPos;

    public GameObject level;

    public int currentWayPoint = 0;
    public Transform targetWayPoint;

    public float speed = 4f;

    // Use this for initialization
    void Start()
    {
        // put the points you want in this variable (points)
        //level = GameObject.Find("LevelManger");
        wayPointList_list = level.GetComponent<LevelConstructor>().splinePoints;

        startPos = wayPointList_list[0];
        targetWayPoint.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        // check if we have somewere to walk
        if (currentWayPoint < this.wayPointList_list.Count)
        {
            if (targetWayPoint == null)
                targetWayPoint.position = wayPointList_list[currentWayPoint];
            walk();
        }
    }

    void walk()
    {
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

    }

    public void EnteredTrigger()
    {
        currentWayPoint++;
        if (currentWayPoint < wayPointList_list.Count)
        {
            targetWayPoint.position = wayPointList_list[currentWayPoint];
        }
        else
        {
            Debug.Log("You reached the goal!?!?");
        }
    }
}
