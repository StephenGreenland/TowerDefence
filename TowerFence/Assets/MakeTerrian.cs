using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTerrian : MonoBehaviour
{
    public GameObject wall;
    
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        for (int i = 0; i < 31; i++)
        {
            Instantiate(wall, new Vector3(i - 1, -.5f, -1),Quaternion.identity);
        }
        for (int i = 0; i < 31; i++)
        {
            Instantiate(wall, new Vector3( - 1, -.5f, i-1),Quaternion.identity);
        }
        for (int i = 0; i < 31; i++)
        {
            Instantiate(wall, new Vector3(i - 1, -.5f, -1),Quaternion.identity);
        }
        for (int i = 0; i < 31; i++)
        {
            Instantiate(wall, new Vector3(i - 1, -.5f, -1),Quaternion.identity);
        }
    }

    
        
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
