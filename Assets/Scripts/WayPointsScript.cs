using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPointsScript : MonoBehaviour
{

    // put the points from unity interface
    public List<Vector3> wayPointList_list = new List<Vector3>();
    public OpponentMovement moveScript;
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
        //transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        //// move towards the target
        //transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        float angle1 = SignedAngle(transform.forward, targetWayPoint.position - transform.position);
        float angle2;
        if (currentWayPoint < wayPointList_list.Count - 1) {
             angle2 = SignedAngle(transform.forward, wayPointList_list[currentWayPoint + 1] - transform.position);
        }
        else
             angle2 = angle1;
        float distToTarget = Vector3.Distance(transform.position, targetWayPoint.position);
        //Debug.Log(angle1);
        if (distToTarget > 5) {
            moveScript.setVel(1 - (angle1 / 90), angle1 / 90);
        }
        else {
            float curInfluence = distToTarget / 5;
            float speed = curInfluence * (1 - ((angle1 / 90)*0.4f)) + (1 - curInfluence) * (1 - ((angle2 / 90) * 0.4f));
            float rot = curInfluence * (angle1 / 90) + (((1 - curInfluence) * (angle2 / 90)) * 100f);

            moveScript.setVel(speed,rot);
            Debug.Log("speed "+ speed);
            Debug.Log("rot " + rot);
        }
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

    float SignedAngle(Vector3 a, Vector3 b) {
        float angle = Vector3.Angle(a, b); // calculate angle
                                           // assume the sign of the cross product's Y component:
        return angle * Mathf.Sign(Vector3.Cross(a, b).y);
    }
}
