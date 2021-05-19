using System.Reflection;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System;
//using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class DragAndShoot : MonoBehaviour
{
    private Vector3 mousePressDown;
    private Vector3 mouseRelease;

    private Vector3 originalPosition;

    private Rigidbody rb;
    private bool isShoot;

    private void Awake()
    {
        
    }

    void Start()
    {
        rb = GameObject.Find("pelota").GetComponent<Rigidbody>();
        rb.useGravity = false;

        originalPosition = transform.position;
        print("Posicion original: " + originalPosition);
    }

    private void OnMouseDown()
    {
        // print("mouseDown");
        mousePressDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        mouseRelease = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.useGravity = true;
        Shoot(mousePressDown - mouseRelease);
        print(mousePressDown);
        print(mouseRelease);
        print(mousePressDown - mouseRelease);
    }

    

    public float forceMultiplier = 3f;

    void Shoot(Vector3 force)
    {
        //print(isShoot);
        // if (isShoot)
        //     return;
        //force = -force;
        print(force * forceMultiplier);
        rb.AddForce(force * forceMultiplier);
        //isShoot = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("space"))
        { 
            transform.position = originalPosition;
            rb.velocity = Vector3.zero; 
            rb.useGravity = false;
            isShoot = false;
            print("Done changing");
        }
    }
}
