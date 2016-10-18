using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPointsScript : MonoBehaviour
{

    // put the points from unity interface
    public Vector3[] wayPointList;
    public List<Vector3> wayPointList_list = new List<Vector3>();

    public Vector3[] points;
    Vector3 startPos;

    GameObject bezier;


    public int currentWayPoint = 0;
    public Transform targetWayPoint;

    public float speed = 4f;

    // Use this for initialization
    void Start()
    {
        // put the points you want in this variable (points)
        bezier = GameObject.Find("BezierSpline");
        points = bezier.GetComponent<BezierSpline>().points;

        wayPointList = points;
        startPos = wayPointList[0];
        targetWayPoint.position = startPos;
        for (int i = 0; i < wayPointList.Length; i++)
        {
            wayPointList_list.Add(wayPointList[i]);
        }
        startPos = wayPointList[0];
        //wayPointList_list.Add(new Vector3(2f, 0 , 0));
        //wayPointList_list.Add(new Vector3(2f, 0, 2f));
        //wayPointList_list.Add(new Vector3(4f, 0, 2f));

        // also add the startpoint as end point
        wayPointList_list.Add(startPos);
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


        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            if(currentWayPoint < wayPointList_list.Count)
            {
                targetWayPoint.position = wayPointList_list[currentWayPoint];
            }
            else
            {
                Debug.Log("You reached the goal!?!?");
            }
        }
    }


}
