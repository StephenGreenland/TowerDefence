using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class avoid : MonoBehaviour
{
    private Transform t;
    public float distance;
    public float turnspeed;
    public Rigidbody rb;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
        t = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        if (Physics.Raycast(t.position, t.forward, out hit, distance))
        {
            Debug.DrawLine(t.position,hit.point,Color.red);
            rb.AddTorque(0,CheckDistance(),0);
        }
        
    }

    private float CheckDistance()
    {
        float tempurn;
        tempurn = turnspeed / hit.distance * 2;

        return tempurn;
    }
}
