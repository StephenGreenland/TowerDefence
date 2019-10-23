using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : MonoBehaviour
{
    private Rigidbody rb;
    public float perlinNoise;
    private int startpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startpoint = Random.Range(0, 1000);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        rb.AddTorque(0,perling(),0);
        
    }

    private float perling()
    {
        
        perlinNoise = Mathf.PerlinNoise(0, startpoint + Time.time);
        return perlinNoise;
    }
    
}
