using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBall : MonoBehaviour
{
    Rigidbody rigidbody;
    public float force = 20f;
    float time = 0f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = time;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

    }


    void OnMouseOver()
    {
        Debug.Log("mouseHover");
        if(timer < 0)
        {
            Vector3 vect = new Vector3(0, 0, -10);

            rigidbody.AddForce(vect * force);
            time = 0.5f;
            timer = time;
        }
        // if (Input.GetKeyDown(KeyCode.Space))
        // {


            
            
        //}
    }
}
