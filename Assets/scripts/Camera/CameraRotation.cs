using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float speedH = 2f;
    public float speedV = 2f;

    private float yaw = 180f;
    private float pitch = 0f;
    Transform t;
    public float fixedRotation = -180;
    Vector3 currentEulerAngles;

    void Start()
    {
        currentEulerAngles += new Vector3(0, -180, 0);

        //apply the change to the gameObject
        transform.eulerAngles = currentEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }
}
