using UnityEngine;
using System.Collections;

public class ActualVehicle : MonoBehaviour {



    public float rotateSpeed = 0.07f;
    public float zRotation;

    // Use this for initialization
    void Start()
    {

    }
    void Awake()
    {
        zRotation = 0;
        GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update () {

        Quaternion newRotation = Quaternion.AngleAxis(-30 * Input.GetAxis("Horizontal"), Vector3.forward);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, newRotation, rotateSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Obstacle")
        {
            transform.parent.transform.GetComponent<RacerScript>().Dead();
        }
        if (collision.transform.tag == "JumpItem")
        {
            Destroy(collision.gameObject);
        }
    }



    public void setRotate(string dir)
    {
        if (dir.Equals("left"))
        {
            zRotation = 30;
        }
        else if (dir.Equals("right"))
        {
            zRotation = -30;
        }
        else if (dir.Equals("center"))
        {
            zRotation = 0;
        }

    }


}
