using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RacerScript : MonoBehaviour {

    private float topForwardSpeed = 0.20f;
    private float topStrafeSpeed = 1.0f;
    public bool isDead = false;
    //private float count = 0;
    public int jumps = 0;
    private float yVel = 0;
    private float zVel = 0;
    private float currentRot = 0;
    private float yAcc;
    private bool flying = false;
    private bool boosting = false;
    private float boostSpeed;
    private float boostTime;
    public GameObject cameraObject;
    private bool isFalling;
    public bool spinning;
    public Rigidbody rb;
    public Animator anim;


    // Use this for initialization
    void Awake () {
        spinning = true;
        //cameraObject = this.transform.Find("Main Camera").gameObject;
    }

    void FixedUpdate() {
        //transform.Translate(0, 0, topForwardSpeed);
        //Vector3 fwd = transform.forward;
        //if (Input.GetKey("up")) {
        //    if (zVel < 6) {
        //        zVel += 0.2f;

        //    }
        //    rb.velocity = fwd * zVel;

        //}
        //else {
        //    if (zVel > 0) {
        //        zVel -= 0.2f;
        //        rb.velocity = fwd * zVel;
        //    }
        //}

        //if (Input.GetKey("down")) {
        //    if (zVel > -2) {
        //        zVel -= 0.2f;

        //    }
        //    rb.velocity = fwd * zVel;

        //}
        //else {
        //    if (zVel < 0) {
        //        zVel += 0.2f;
        //    }
        //    rb.velocity = fwd * zVel;

        //}
        if (spinning) {
            Vector3 fwd = transform.forward;
            if (Input.GetKey("up")) {
                //if (rb.velocity.z < 50) {

                //}
                rb.AddRelativeForce(0, 0, 10);

                //rb.velocity = fwd * zVel;

            }
            else {
                //if (zVel > 0) {
                //    zVel -= 0.2f;
                //    rb.velocity = fwd * zVel;
                //}
            }

            if (Input.GetKey("down")) {
                if (zVel > -2) {
                    zVel -= 0.2f;

                }
                rb.velocity = fwd * zVel;

            }
            //else {
            //    if (zVel < 0) {
            //        zVel += 0.2f;
            //    }
            //    rb.velocity = fwd * zVel;

            //}

            //if (Input.GetKey("right")) {
            //    transform.Rotate(0, 0.3f * rb.velocity.magnitude, 0);
            //}

            //if (Input.GetKey("left")) {
            //    transform.Rotate(0, -0.3f * rb.velocity.magnitude, 0);
            //}

            transform.Rotate(0, 0.3f * rb.velocity.magnitude * Input.GetAxis("Horizontal"), 0);
        }
        //Debug.Log(rb.velocity.z);
    }


    // Update is called once per frame
    void Update() {





        //transform.Rotate(0, 2, 0);






        //transform.eulerAngles = new Vector3(0, 30 * Input.GetAxis("Horizontal"), 0);



        if (!isDead)
        {
            /////////////////////////////////
            //Jumping
            /////////////////////////////////
            if((Input.GetKeyDown("space") || Input.GetButtonDown("Fire2")) && jumps > 0)
            {
                jumps--;
                flying = true;
                yVel = 2;
                yAcc = 0.05f;
            }

            if (flying)
            {
                transform.Translate(0, yVel, 0);
                yVel -= yAcc;
                if (yVel < -0.4)
                {
                    yAcc = 0.0f;


                }
                if (this.transform.position.y <= 0)
                {
                    flying = false;
                    this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                }
            }
            /////////////////////////////////
            //Boosting
            /////////////////////////////////
            if (boosting)
            {
                boostTime += Time.deltaTime;
                transform.Translate(0, 0, boostSpeed);
                if (boostTime >= 3) {
                    boostSpeed -= 0.1f;
                    if (boostSpeed <= 0)
                    {
                        boosting = false;
                    }
                    if (cameraObject.GetComponent<Camera>().fieldOfView > 60)
                    {
                        cameraObject.GetComponent<Camera>().fieldOfView -= 0.25f;
                    }
                }
                else if (cameraObject.GetComponent<Camera>().fieldOfView < 70)
                {
                    cameraObject.GetComponent<Camera>().fieldOfView += 0.25f;
                }
            }
            else if (cameraObject.GetComponent<Camera>().fieldOfView > 60)
            {
                cameraObject.GetComponent<Camera>().fieldOfView -= 0.25f;
            }

            /////////////////////////////////
            //Turning
            /////////////////////////////////

            //transform.Translate(topStrafeSpeed * Input.GetAxis("Horizontal"), 0, 0);
            if (Input.GetButtonDown("Fire2"))
                Debug.Log(Input.GetButtonDown("Fire2"));


            //if (Input.GetKey("left") || Input.GetKey("a"))
            //{
            //    transform.Translate(-topStrafeSpeed, 0, 0);
            //    transform.FindChild("ActualVehicle").GetComponent<ActualVehicle>().setRotate("left");
            //}
            //else if (Input.GetKey("right") || Input.GetKey("d"))
            //{
            //    transform.FindChild("ActualVehicle").GetComponent<ActualVehicle>().setRotate("right");
            //    transform.Translate(topStrafeSpeed, 0, 0);
            //}
            //else
            //{
            //    transform.FindChild("ActualVehicle").GetComponent<ActualVehicle>().setRotate("center");
            //}

            if (Mathf.Abs(transform.position.x) > 50 && transform.position.y <= 0 && !isFalling)
            {
                isFalling = true;
                isDead = true;
            }
            if(isFalling)
            {
                transform.Translate(0, -3, 0);
                if(!isDead)
                     isDead = true;

            }
        }
        else
        {
            //count += Time.deltaTime;
            if (isFalling)
            {
                transform.Translate(0, -3, 0);
                topForwardSpeed = 0;


            }

            if ((Input.GetKeyDown("space") || Input.GetButtonDown("Fire2")))
                SceneManager.LoadScene("DefaultScene");
        }



    }

    public void Dead()
    {
        Debug.Log("Died");
        isDead = true;
        GameObject explosion = Resources.Load("Explosion2") as GameObject;
        explosion.transform.position = transform.FindChild("ActualVehicle").transform.position;
        explosion.transform.Translate(0, -1.23f, -1.0f);
        Instantiate(explosion);
        topForwardSpeed = 0;
        Destroy(transform.FindChild("ActualVehicle").gameObject);
    }

    public void addJump()
    {
        Debug.Log("jump");
        jumps++;
    }

    public void activateBoost()
    {
        boostSpeed = 2;
        boosting = true;
        boostTime = 0;
    }

    public int getJumps()
    {
        return jumps;
    }

    public void hitBanana() {
        StartCoroutine(holdControls());
    }
    IEnumerator holdControls() {
        spinning = false;

        //yield return new WaitUntil(anim.GetCurrentAnimatorStateInfo(0).IsName("CanMove"));
        //yield return new WaitUntil(System.Func < T, anim.GetCurrentAnimatorStateInfo(0).IsName("CanMove") >);
        yield return new WaitForSeconds(2.50001f);
        transform.FindChild("GameObject").GetComponent<Animator>().enabled = false;
        spinning = true;

        //new predicate<T>(func<T, bool>)
    }
}