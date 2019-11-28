using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Vector2 currentPosition;
    public int height;
    public int width;
    public GameObject wall;
    
    public int[,] grid;
    private bool[,] vistedBefore;
    
    public List<Vector2> finalPath;
    
    private int ballout;
    
 
    
    private Vector2 lastSpot;

    public LayerMask Notwall;
    public LayerMask isWall;
    

    // Start is called before the first frame update
    void Start()
    {
               
        finalPath = new List<Vector2>();
        vistedBefore = new bool[width, height];
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                vistedBefore[i, j] = false;
            }
        }

        grid = new int[width, height];
    }
    private void Update()
    {
        

    }

  /*  private void OnDrawGizmos()
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
    */
    public List<Vector2> FindPath(Vector2 startPos, Vector2 endPos)
    {
        finalPath.Clear();
        currentPosition = startPos;
        while (currentPosition != endPos)
        {
            currentPosition = MoveTo(CheckVaildSpaces(),endPos);
            ballout++;
            if (ballout > 100000)
            {
                
                break;
            }
        }
        if (currentPosition == endPos)
        {
            return finalPath;
        }

        return null;
    }
    private List<Vector2> CheckVaildSpaces()
    {
        List<Vector2> isVaild;
        isVaild = new List<Vector2>();

        // check 4 directions
        if (currentPosition.x != width - 1)
        {
            if (grid[(int) currentPosition.x + 1, (int) currentPosition.y] != 1 &&
                vistedBefore[(int) currentPosition.x + 1, (int) currentPosition.y] == false)
            {
                isVaild.Add(new Vector2(currentPosition.x + 1, currentPosition.y));
            }
        }

        if (currentPosition.y != height - 1)
        {
            if (grid[(int) currentPosition.x, (int) currentPosition.y + 1] != 1 &&
                vistedBefore[(int) currentPosition.x, (int) currentPosition.y + 1] == false)
            {
                isVaild.Add(new Vector2(currentPosition.x, currentPosition.y + 1));
            }
        }

        if (currentPosition.y != 0)
        {
            if (grid[(int) currentPosition.x, (int) currentPosition.y - 1] != 1 &&
                vistedBefore[(int) currentPosition.x, (int) currentPosition.y - 1] == false)
            {
                isVaild.Add(new Vector2(currentPosition.x, currentPosition.y - 1));
            }
        }

        if (currentPosition.x != 0)
        {
            if (grid[(int) currentPosition.x - 1, (int) currentPosition.y] != 1 &&
                vistedBefore[(int) currentPosition.x - 1, (int) currentPosition.y] == false)
            {
                isVaild.Add(new Vector2(currentPosition.x - 1, currentPosition.y));
            }
        }
        return isVaild;
    }
    private Vector2 MoveTo(List<Vector2> isVaild, Vector2 endPos)
    {
        Vector2 whereToMove;
        whereToMove = new Vector2(currentPosition.x, currentPosition.y);
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

            vistedBefore[(int) currentPosition.x, (int) currentPosition.y] = true;
            finalPath.Add(new Vector2(currentPosition.x, currentPosition.y));
            
            return whereToMove;
        }

        vistedBefore[(int) currentPosition.x, (int) currentPosition.y] = true;
        whereToMove = finalPath[finalPath.Count-1];
        finalPath.RemoveAt(finalPath.Count - 1);
        return whereToMove;
    }

    
}
