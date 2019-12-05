using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    public List<GameObject> targets;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBase>() != null)
        {
            if (targets != null) targets.Add(other.gameObject);

            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyBase>() != null)
        {
            if (targets != null) targets.Remove(other.gameObject);

            
        }
    }
}
