using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public Health health;
    
    // Start is called before the first frame update
    void Start()
    {
        health.OutOfHealth += Lose;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyBase>() != null)
        {
            health.Change(-1);
            
        }
    }

    public void Lose()
    {
        
        //say you lose
        // go to main menu
        Debug.Log("you lose");

    }

    private void OnDestroy()
    {
        health.OutOfHealth -= Lose;
    }
}
