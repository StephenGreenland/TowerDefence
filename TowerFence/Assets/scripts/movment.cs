using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{

    private Rigidbody rb;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddRelativeForce(0,0,speed);
    }
}
