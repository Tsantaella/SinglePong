using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    public float force = 40f;
    GameObject prefab;
    GameObject pelota;
    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load("pelota") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(GameObject.Find("pelota(Clone)"))
            {
                Destroy(pelota);
            }
            pelota = Instantiate(prefab) as GameObject;
            pelota.transform.position = transform.position + transform.forward;
            Rigidbody rb = pelota.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * force;
        }
    }
}
