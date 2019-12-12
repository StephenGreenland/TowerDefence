using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnit : EnemyBase
{
    public Vector2[] path;
    public PathFinder Grid;
    public Click ClickManager;
    public Health myhealth;
    public float speed;
    
        
    private int count;
    private float timer;
    private Vector2 Endpos;
    private float minDistance;
    private GameObject castle;
    public event Action<GameObject> OnDeath;
    
    public enum  myEnum
    {
        purple,red
    }

    public myEnum colour;
    
    // Start is called before the first frame update
    void Start()
    {
        if (colour == myEnum.red)
        {
            castle = GameObject.FindWithTag("red");
            
        }

        if (colour == myEnum.purple)
        {
            castle = GameObject.FindWithTag("purple");
        }
        
        
        
        ClickManager = GameObject.Find("ClickManager").GetComponent<Click>();
        Grid = GameObject.Find("Grid").GetComponent<PathFinder>();

        myhealth.OutOfHealth += Die;
        Click.OnCreateStatic += NewPath;
        
        Endpos = Grid.endPos;
        
        count = 0;
        timer = 1;

        Endpos = new Vector2((int)Mathf.Round(castle.transform.position.x), (int)Mathf.Round(castle.transform.position.z));
        path = Grid.FindPath(new Vector2(transform.position.x,transform.position.z),Endpos);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count < path.Length )
        {
            
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[count].x, 0, path[count].y), Time.deltaTime*speed);

            if (Vector3.Distance(gameObject.transform.position,new Vector3(path[count].x,0,path[count].y)) < .1f)
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
        myhealth.OutOfHealth += Die;
        OnDeath?.Invoke(gameObject);

    }
    private void Die()
    {
        Destroy(gameObject);

    }


}
