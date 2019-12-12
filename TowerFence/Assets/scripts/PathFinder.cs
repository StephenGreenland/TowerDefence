using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
   
    public int height;
    public int width;
    public GameObject Castle;
    
    
    public int[,] grid;
    private bool[,] vistedBefore;
    
    private List<Vector2> finalPath;
    
    private int bailout;

    public Vector2 endPos;
    
    // click stuff
    public LayerMask Notwall;
    public LayerMask isWall;

    public Vector2 lastPlace;

    // for scan
    public float gridExtents = 0.5f;
    public float startHeight;

    // Start is called before the first frame update
    void OnEnable()
    {
              
        finalPath = new List<Vector2>();
        vistedBefore = new bool[width, height];
        
        ClearIsValid();

        grid = new int[width, height];
        scan();
       // Instantiate(Castle, new Vector3(endPos.x-1, 0, endPos.y-1), Quaternion.identity);
        endPos = new Vector2((int)Mathf.Round(Castle.transform.position.x),(int)Mathf.Round(Castle.transform.position.z));
        grid[(int)endPos.x,(int)endPos.y] = 0;
    }

    private void ClearIsValid()
    {
        bailout = 0;
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                vistedBefore[i, j] = false;
            }
        }
    }

    private void Update()
    {
        

    }

    private void OnDrawGizmos()
    {
        if (finalPath != null)
            foreach (Vector2 pathPoint in finalPath)
            {
                Gizmos.color = new Color(0f, 1f, 0f, 0.75f);
                Gizmos.DrawCube(new Vector3(pathPoint.x, 0, pathPoint.y), Vector3.one);
            }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Gizmos.color = new Color(0f, 1f, 1f, 0.75f);
                Gizmos.DrawLine(new Vector3(i,0,j),new Vector3(i,.5f,j) );
                if (vistedBefore != null && vistedBefore[i, j])
                {
                    Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
                    Gizmos.DrawCube(new Vector3(i, 0, j), new Vector3(0.5f, 0.5f, 0.5f));
                }
            }
        }
    } 
    
    public Vector2[] FindPath(Vector2 currentPos, Vector2 endPos)
    {
        finalPath.Clear();
        ClearIsValid();
        

        currentPos.x = Mathf.Round(currentPos.x);
        currentPos.y = Mathf.Round(currentPos.y);

        endPos.x = Mathf.Round(endPos.x);
        endPos.y = Mathf.Round(endPos.y);

        while (currentPos != endPos)
        {
            currentPos = MoveTo(CheckVaildSpaces(currentPos),endPos,currentPos);
            bailout++;
            if (bailout > 1000)
            {
                
                break;
            }
        }
        if (currentPos == endPos)
        {
            return finalPath.ToArray();
        }

        return null;
    }
    private List<Vector2> CheckVaildSpaces(Vector2 startPos)
    {
        List<Vector2> isVaild;
        isVaild = new List<Vector2>();

        // check 4 directions
        if (startPos.x != width - 1)
        {
            if (grid[(int) startPos.x + 1, (int) startPos.y] != 1 &&
                vistedBefore[(int) startPos.x + 1, (int) startPos.y] == false)
            {
                isVaild.Add(new Vector2(startPos.x + 1, startPos.y));
            }
        }

        if (startPos.y != height - 1)
        {
            if (grid[(int) startPos.x, (int) startPos.y + 1] != 1 &&
                vistedBefore[(int) startPos.x, (int) startPos.y + 1] == false)
            {
                isVaild.Add(new Vector2(startPos.x, startPos.y + 1));
            }
        }

        if (startPos.y != 0)
        {
            if (grid[(int) startPos.x, (int) startPos.y - 1] != 1 &&
                vistedBefore[(int) startPos.x, (int) startPos.y - 1] == false)
            {
                isVaild.Add(new Vector2(startPos.x, startPos.y - 1));
            }
        }

        if (startPos.x != 0)
        {
            if (grid[(int) startPos.x - 1, (int) startPos.y] != 1 &&
                vistedBefore[(int) startPos.x - 1, (int) startPos.y] == false)
            {
                isVaild.Add(new Vector2(startPos.x - 1, startPos.y));
            }
        }
        return isVaild;
    }
    private Vector2 MoveTo(List<Vector2> isVaild, Vector2 endPos ,Vector2 startPos)
    {
        Vector2 whereToMove;
        whereToMove = new Vector2(startPos.x, startPos.y);
        if (isVaild.Count > 0)
        {
            for (int i = 0; i < isVaild.Count; i++)
            {
                if (i == 0)
                {
                    whereToMove = isVaild[0];
                }

                if (Vector2.Distance(isVaild[i], endPos) < Vector2.Distance(whereToMove, endPos))
                {
                    whereToMove = isVaild[i];
                }
            }

            vistedBefore[(int) startPos.x, (int) startPos.y] = true;
            finalPath.Add(new Vector2(startPos.x, startPos.y));
            
            return whereToMove;
        }

        vistedBefore[(int) startPos.x, (int) startPos.y] = true;

        if (finalPath.Count>0)
        {
            whereToMove = finalPath[finalPath.Count-1];
            finalPath.RemoveAt(finalPath.Count - 1);
        }
        else
        {
            Debug.Log("OUT OF BOUNDS... WAAAA?!!");   
        }
        return whereToMove;
    }

    public void scan()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Physics.CheckBox(new Vector3(x, startHeight, y), new Vector3(gridExtents, gridExtents, gridExtents), Quaternion.identity))
                {
                    // Something is there
                    grid[x, y] = 1;
                }
            }
        }


    }
    
    
}
