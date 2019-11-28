using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ItemBase
{
    public PathFinder grid;
    public List<GameObject> targets;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targets != null && targets.Count > 0)
        {
            transform.LookAt(targets[ChoseEnemy()].transform);
           
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBase>() != null)
        {
            if (targets != null) targets.Add(other.gameObject);
            
        }
    }

    private int ChoseEnemy()
    {
       return Random.Range(0, (targets.Count-1));
    }
}
