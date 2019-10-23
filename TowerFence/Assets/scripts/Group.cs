using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public List<GameObject> Friends;
    private Quaternion qAverage;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        // Friends = new List<Quaternion>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindRotation();
        rb.AddTorque(qAverage.x,qAverage.y,qAverage.z);

    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Group>() != null)
        {
            if (Friends != null) Friends.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Group>() != null)
        {
            Friends.Remove(other.gameObject);
        }
         
    }

    private void FindRotation()
    {
        if (Friends != null)
            for (int i = 0; i < Friends.Count; i++)
            {
                Quaternion q = Friends[i].transform.rotation;

                qAverage *= Quaternion.Slerp(Quaternion.identity, q, Friends.Count);
            }
        Debug.Log(qAverage);
        
    }
}
