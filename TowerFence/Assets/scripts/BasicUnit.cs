using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnit : EnemyBase
{
    public List<Vector2> path;
    public PathFinder Grid;
    public Click ClickManager;
    private int count;

    private float timer;

    private Vector2 Endpos;

    private float minDistance;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Endpos = Grid.endPos;
        Click.OnCreateStatic += NewPath;
        count = 0;
        path = Grid.FindPath(new Vector2(transform.position.x,transform.position.z),Endpos);
        timer = 1;
         

    }

    // Update is called once per frame
    void Update()
    {
        
        

        if (count < path.Count - 1)
        {
            
            
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[count].x, 0, path[count].y), Time.deltaTime);

            if (Vector3.Distance(gameObject.transform.position,new Vector3(path[count].x,0,path[count].y)) < .2f)
            {
                
                count++;
            }
        }
        


    }

    private void NewPath()
    {
        path = Grid.FindPath(new Vector2(transform.position.x, transform.position.z),Endpos);
        count = 0;
    }

    private void OnDestroy()
    {
        Click.OnCreateStatic -= NewPath;
    }
}
