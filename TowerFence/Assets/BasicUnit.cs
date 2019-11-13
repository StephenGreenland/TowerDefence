using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnit : MonoBehaviour
{
    public List<Vector2> path;
    public scanner Grid;

    private int count;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[count].x, 0, path[count].y), 1f);
        count++;


    }
    
    
}
