using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanner : MonoBehaviour
{
    public Vector2 currentPosition;
    public int height;
    public int width;
    
    private int[,] grid;
    private bool[,] vistedBefore;
    private List<Vector2> finalPath;
    private int ballout;
    public Vector2 startPos;
    public Vector2 endPos;
    
    private Vector2 lastSpot;
    

    // Start is called before the first frame update
    void Start()
    {
        
        currentPosition = startPos;
        
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
        FindPath();

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
                if (vistedBefore != null && vistedBefore[i, j])
                {
                    Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
                    Gizmos.DrawCube(new Vector3(i, 0, j), new Vector3(0.5f, 0.5f, 0.5f));
                }
            }
        }
    }
    private void FindPath()
    {
        while (currentPosition != endPos)
        {
            currentPosition = MoveTo(CheckVaildSpaces());
            ballout++;
            if (ballout > 100000)
            {
                Debug.Log("imaBreak");
                break;
            }
        }
        if (currentPosition == endPos)
        {
            print("youwin");
            // pass on list (finalpath) to who ever needs it
        }
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
    private Vector2 MoveTo(List<Vector2> isVaild)
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
            print(whereToMove);
            return whereToMove;
        }

        vistedBefore[(int) currentPosition.x, (int) currentPosition.y] = true;
        whereToMove = finalPath[finalPath.Count-1];
        finalPath.RemoveAt(finalPath.Count - 1);
        return whereToMove;
    }
    private void Reset()
    {
        
        currentPosition = startPos;
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                vistedBefore[i, j] = false;
            }
        }
        finalPath.Clear();
    }

    public void Scan()
    {
        Reset();
        // Scan the real world starting at 0,0,0 (to be able to place the grid anywhere, add transform.position)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Physics.CheckBox(new Vector3(x, 0, y), new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
                {
                    // Something is there
                    grid[x, y] = 1;
                }
            }
        }
    }
    
}
