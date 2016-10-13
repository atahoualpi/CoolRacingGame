using UnityEngine;
using System.Collections;

public class RacerScript : MonoBehaviour {

    private float topForwardSpeed = 2.0f;
    private float topStrafeSpeed = 1.0f;
    public bool isDead = false;
    //private float count = 0;
    public int jumps = 0;
    private float yVel = 0;
    private float yAcc;
    private bool flying = false;
    private bool boosting = false;
    private float boostSpeed;
    private float boostTime;
    GameObject cameraObject;
    private bool isFalling;



    // Use this for initialization
    void Awake () {
        cameraObject = this.transform.Find("Main Camera").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, topForwardSpeed);
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

            transform.Translate(topStrafeSpeed * Input.GetAxis("Horizontal"), 0, 0);
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
                Application.LoadLevel("DefaultScene");
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
}