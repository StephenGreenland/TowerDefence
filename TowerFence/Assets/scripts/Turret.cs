using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : ItemBase
{
    public PathFinder grid;
    public List<GameObject> targets;
    private GameObject target;
    public int damage;

    private float cooldown;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        if (target == null && targets !=null)
        {
            target = targets[ChoseEnemy()];

        }
        
        if (targets != null && targets.Count > 0)
        {
            
            transform.LookAt(target.transform);

            if (cooldown < 0)
            {
                target.GetComponent<Health>().Change(damage);
                
                // DO LIKE A SICK FIRE ANIMATION

                cooldown = 2f;

            }
           
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBase>() != null)
        {
            if (targets != null) targets.Add(other.gameObject);

            target = null;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyBase>() != null)
        {
            if (targets != null) targets.Remove(other.gameObject);

            

        }
    }

    private int ChoseEnemy()
    {
       return Random.Range(0, (targets.Count-1));
    }
}
