using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnit : MonoBehaviour
{
    private List<Vector2> path;
    public scanner Grid;

    private int count;

    private float timer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        path = Grid.finalPath;
        timer = 1;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[count].x, 0, path[count].y), 1f);

        if (timer < 0)
        {
            timer = .5f;
            count++;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[count].x, 0, path[count].y), 1f);

        }
        


    }
    
    
}
