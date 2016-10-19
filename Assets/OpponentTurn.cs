using UnityEngine;
using System.Collections;

public class OpponentTurn : MonoBehaviour {
    public float rotateSpeed = 0.07f;
    public float rot;

    // Use this for initialization
    void Start () {
    }

// Update is called once per frame
    void Update () {
        Quaternion newRotation = Quaternion.AngleAxis(-30 * rot, Vector3.forward);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, newRotation, rotateSpeed);
    }
}
