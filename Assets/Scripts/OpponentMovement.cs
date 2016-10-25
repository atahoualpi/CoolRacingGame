using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpponentMovement : MonoBehaviour {

    //private float count = 0;
    public int jumps = 0;
    private float maxSpeed = 7;
    private float currentRot = 0;
    private float currentSpeed = 0;
    public Rigidbody rb;
    public OpponentTurn turn;
    public bool spinning;
    public Animator anim;
    private float acc;
    private bool boosting = false;
    private float boostTime;


    // Use this for initialization
    void Awake () {
            spinning = true;
            acc = 10;
            //cameraObject = this.transform.Find("Main Camera").gameObject;
    }

    public void Boost() {
        acc = 20;
        StopCoroutine(Boosting());
        StartCoroutine(Boosting());
    }

    IEnumerator Boosting() {
        boosting = true;
        boostTime = 0;
        yield return new WaitForSeconds(3f);
        acc = 10;
    }

    void FixedUpdate() {
        if (spinning) {
            //if (rb.velocity.magnitude < currentSpeed*maxSpeed) {
            rb.AddRelativeForce(0, 0, acc);

            //}


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


            //ORIGINAL
            transform.Rotate(0, 0.3f * rb.velocity.magnitude * (currentRot), 0);

            if (rb.velocity.magnitude < 2f) {
                transform.Rotate(0, 2f * Mathf.Sign(currentRot), 0);
            }
            //transform.Rotate(0, 0.5f * transform.InverseTransformDirection(rb.velocity).z * (currentRot), 0);
            //Debug.Log(currentRot);

            turn.rot = currentRot / 1.3f;
        }
        //Debug.Log(rb.velocity.z);
    }


    // Update is called once per frame
    void Update() {


    }

    public void setVel(float speed, float rot) {
        currentSpeed = speed;
        currentRot = rot;
    }

    void OnCollisionEnter(Collision col) {
        //Debug.Log("HIT WALL");

        if (col.gameObject.tag == "Wall") {
            GetComponent<WayPointsScript>().addHit();
        }
    }

    public void hitBanana() {
        StartCoroutine(holdControls());
    }
    IEnumerator holdControls() {
        spinning = false;
        //Debug.Log("dwadwa");
        //yield return new WaitUntil(anim.GetCurrentAnimatorStateInfo(0).IsName("CanMove"));
        //yield return new WaitUntil(System.Func < T, anim.GetCurrentAnimatorStateInfo(0).IsName("CanMove") >);
        yield return new WaitForSeconds(2.50001f);
        transform.FindChild("GameObject").GetComponent<Animator>().enabled = false;
        spinning = true;

        //new predicate<T>(func<T, bool>)
    }
}
