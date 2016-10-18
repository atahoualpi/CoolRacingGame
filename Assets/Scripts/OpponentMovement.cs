using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpponentMovement : MonoBehaviour {

    //private float count = 0;
    public int jumps = 0;
    private float maxSpeed = 6;
    private float currentRot = 0;
    private float currentSpeed = 0;
    public Rigidbody rb;

    

    // Use this for initialization
    void Awake () {
        //cameraObject = this.transform.Find("Main Camera").gameObject;
    }

    void FixedUpdate() {

            if (rb.velocity.magnitude < currentSpeed/maxSpeed) {
                rb.AddRelativeForce(0, 0, 10);

            }
        

        //if (Input.GetKey("down")) {
        //    if (zVel > -2) {
        //        zVel -= 0.2f;

        //    }
        //    rb.velocity = fwd * zVel;

        

        //if (Input.GetKey("right")) {
        //    transform.Rotate(0, 0.3f * rb.velocity.magnitude, 0);
        //}

        //if (Input.GetKey("left")) {
        //    transform.Rotate(0, -0.3f * rb.velocity.magnitude, 0);
        //}

        transform.Rotate(0, 0.3f * rb.velocity.magnitude * currentRot, 0);

        //Debug.Log(rb.velocity.z);
    }


    // Update is called once per frame
    void Update() {


    }

    public void setVel(float speed, float rot) {
        currentSpeed = speed;
        currentRot = rot;
    }
}
