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
    
    
    // Start is called before the first frame update
    void Start()
    {
        Endpos = new Vector2(20,20);
        ClickManager.OnCrate += NewPath;
        count = 0;
        path = Grid.FindPath(new Vector2(Mathf.Round(transform.position.x),Mathf.Round( transform.position.z)),Endpos);
        timer = 1;
         

    }

    // Update is called once per frame
    void Update()
    {
        
        
        timer -= Time.deltaTime;
        
        

        if (timer < 0 && count < path.Count)
        {
            timer = .5f;
            count++;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[count].x, 0, path[count].y), 1f);
            
                
        }
        


    }

    private void NewPath()
    {
        path = Grid.FindPath(new Vector2(transform.position.x, transform.position.z),Endpos);
        count = 0;
    }

    private void OnDestroy()
    {
        ClickManager.OnCrate -= NewPath;
    }
}
