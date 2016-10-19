using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPointsScript : MonoBehaviour {

    public List<Vector3> wayPointList_list = new List<Vector3>();
    public OpponentMovement moveScript;
    public bool isOpponent;
    Vector3 startPos;
    public int currentLap;
    public GameObject level;
    private List<Vector3> splines = new List<Vector3>();
    public int currentWayPoint = 0;
    public Transform targetWayPoint;




    //RANDOM VARIABLE AI
    private float speedVar;
    private float distVar;
    private float rotVar;
    private int wallHits;

    private float lapTime;
    private float t;


    // Use this for initialization
    void Start() {
        // put the points you want in this variable (points)
        //level = GameObject.Find("LevelManger");
        createLaps(10);
    }
    void Awake() {
        t = 0;
        lapTime = 0;
        currentLap = 0;
    }

    void createLaps(int laps) {
        splines.AddRange(level.GetComponent<LevelConstructor>().splinePoints);
        splines.RemoveAt(splines.Count-1);

        for (int i = 0; i < laps; i++) {
            wayPointList_list.AddRange(splines);
        }

        wayPointList_list.Add(wayPointList_list[0]);
        startPos = wayPointList_list[0];
        targetWayPoint.position = startPos;
    }

    // Update is called once per frame
    void Update() {
        t += Time.deltaTime;
        // check if we have somewere to walk
        if (currentWayPoint < this.wayPointList_list.Count) {
            if (targetWayPoint == null)
                targetWayPoint.position = wayPointList_list[currentWayPoint];
            if(isOpponent)
                walk();
        }
    }

    void walk() {
        // rotate towards the target
        //transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        //// move towards the target
        //transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);
        
        float angle1 = SignedAngle(transform.forward, targetWayPoint.position - transform.position);
        float angle2;
        Vector3 pointsV = wayPointList_list[currentWayPoint + 1] - targetWayPoint.position;

        float anglePoints = SignedAngle(targetWayPoint.position, wayPointList_list[currentWayPoint + 1]);
        if (currentWayPoint < wayPointList_list.Count - 1) {
            angle2 = SignedAngle(transform.forward, wayPointList_list[currentWayPoint + 1] - transform.position);
        }
        else
            angle2 = angle1;
        float distToTarget = Vector3.Distance(transform.position, targetWayPoint.position);
        //Debug.Log(angle1);
        if (distToTarget > distVar) {

            //moveScript.setVel(1 - (angle1 / 90), angle1 / 90);      
            moveScript.setVel(1, angle1 / 90);
        }
        else {
            //float curInfluence = distToTarget / 7;
            float speed = ((1 - (SignedAngle(transform.forward, pointsV) / 90f)) / 4) + 0.25f;
            speed = 1;
            float rot = SignedAngle(transform.forward, pointsV)*rotVar / 90f;
            //float speed = curInfluence * (1 - ((angle1 / 90)*0.4f)) + (1 - curInfluence) * (1 - ((angle2 / 90) * 0.4f));
            //float rot = curInfluence * (angle1 ) + (((1 - curInfluence) * (angle2)));

            moveScript.setVel(1, rot);
            //Debug.Log("speed " + speed);
            //Debug.Log("rot " + rot);
        }
    }

    public void newLap() {
        Debug.Log(this.name);
        Debug.Log("LAP TIME: " + t);
        Debug.Log("DIST VAR: " + distVar);
        Debug.Log("ROT VAR: " + rotVar);
        t = 0;

        currentLap++;
        distVar = Random.Range(4f, 8f);
        rotVar = Random.Range(1f, 6f);

        //Debug.Log(currentLap);
    }

    public void EnteredTrigger() {
        currentWayPoint++;
        if (currentWayPoint < wayPointList_list.Count) {
            targetWayPoint.position = wayPointList_list[currentWayPoint];
        }
        else {
            Debug.Log("You reached the goal!?!?");
        }
    }

    float SignedAngle(Vector3 a, Vector3 b) {
        float angle = Vector3.Angle(a, b); // calculate angle
                                           // assume the sign of the cross product's Y component:
        return angle * Mathf.Sign(Vector3.Cross(a, b).y);
    }

    public float getDistToNextWP() {
        return Vector3.Distance(transform.position, targetWayPoint.position);
    }

    public void addHit() {

    }
}
