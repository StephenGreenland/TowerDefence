using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Click : MonoBehaviour
{

    public GameObject[] itemSelect;
    
    public GameObject wall;
    public GameObject turret;

    private int selected;

    [FormerlySerializedAs("scanner")] public PathFinder pathFinder;

    public LayerMask Notwall;
    public LayerMask isWall;

    public event Action OnCrate;

    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        selected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, Notwall))
            {
                if (pathFinder.grid[(int) Mathf.Round(hit.point.x), (int) Mathf.Round(hit.point.z)] == 0)
                {
                    pathFinder.grid[(int) Mathf.Round(hit.point.x), (int) Mathf.Round(hit.point.z)] = 1;
                    
                    Instantiate(itemSelect[selected], new Vector3((int) Mathf.Round(hit.point.x), 0, (int) Mathf.Round(hit.point.z)),
                        transform.rotation);
                    //pathFinder.Reset();
                    OnCrate.Invoke();
                }
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000,isWall))
            {
                pathFinder.grid[(int) Mathf.Round(hit.collider.transform.position.x),
                    (int) Mathf.Round(hit.collider.transform.position.z)] = 0;
                Destroy(hit.collider.gameObject);
            
                
            } 
            
        }
        
    }

    public void WallSelect()
    {
        selected = 0;

    }
    public void TurretSelect()
    {
        selected = 1;

    }
    
}

