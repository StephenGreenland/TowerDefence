using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Click : MonoBehaviour
{

    public GameObject[] itemSelect;
    public Text uiMoney;
    private int selected;

    [FormerlySerializedAs("scanner")] public PathFinder pathFinder;

    public LayerMask Notwall;
    public LayerMask isWall;

    public static event Action OnCreateStatic;
    
    
    public int turrentcost;
    public int wallcost;
    public int money;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        selected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        uiMoney.text = ("Money ")+money.ToString();
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, Notwall))
            {
                if (pathFinder.grid[(int) Mathf.Round(hit.point.x), (int) Mathf.Round(hit.point.z)] == 0)
                {
                    
                    
                    if(selected == 1&& money > turrentcost )
                    {
                        Instantiate(itemSelect[selected], new Vector3((int)Mathf.Round(hit.point.x), 0, (int)Mathf.Round(hit.point.z)),
                        transform.rotation);

                        pathFinder.grid[(int)Mathf.Round(hit.point.x), (int)Mathf.Round(hit.point.z)] = 1;
                        money -= turrentcost;
                    }

                    if (selected == 0 && money > wallcost)
                    {
                        Instantiate(itemSelect[selected], new Vector3((int)Mathf.Round(hit.point.x), 0, (int)Mathf.Round(hit.point.z)),
                        transform.rotation);

                        pathFinder.grid[(int)Mathf.Round(hit.point.x), (int)Mathf.Round(hit.point.z)] = 1;

                        money -= wallcost;
                    }


                    OnCreateStatic?.Invoke();

                    pathFinder.lastPlace = new Vector2(Mathf.Round(hit.point.x), Mathf.Round(hit.point.z));
                }
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000,isWall))
            {
                if (hit.collider.gameObject.tag == "Wall" )
                {
                    money += 1;

                }
                if (hit.collider.gameObject.tag == "Tower")
                {
                    money += 3;

                }
                pathFinder.grid[(int) Mathf.Round(hit.collider.transform.position.x),
                    (int) Mathf.Round(hit.collider.transform.position.z)] = 0;
                Destroy(hit.collider.gameObject);

                OnCreateStatic?.Invoke();
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

