using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

    public event Action OutOfHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            ImDead();

        }
    }

    public void Change(int amount)
    {
        health += amount;


    }

    void ImDead()
    {
        OutOfHealth?.Invoke();
    }
    
    
}
