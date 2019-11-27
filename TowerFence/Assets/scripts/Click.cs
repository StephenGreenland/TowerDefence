using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

    public GameObject[] itemSelect;
    
    public GameObject wall;
    public GameObject turret;

    private int selected;

    public scanner scanner;

    public LayerMask Notwall;
    public LayerMask isWall;

    
    
    
    
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
                if (scanner.grid[(int) Mathf.Round(hit.point.x), (int) Mathf.Round(hit.point.z)] == 0)
                {
                    scanner.grid[(int) Mathf.Round(hit.point.x), (int) Mathf.Round(hit.point.z)] = 1;
                    
                    Instantiate(itemSelect[selected], new Vector3((int) Mathf.Round(hit.point.x), 0, (int) Mathf.Round(hit.point.z)),
                        transform.rotation);
                    scanner.Reset();
                }
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000,isWall))
            {
                scanner.grid[(int) Mathf.Round(hit.collider.transform.position.x),
                    (int) Mathf.Round(hit.collider.transform.position.z)] = 0;
                Destroy(hit.collider.gameObject);
            
                scanner.Reset();
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

