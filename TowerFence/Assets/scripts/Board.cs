using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public Vector2 currentPosition;
    public int height;
    public int width;
    public GameObject start;
    public GameObject end;
    public int invDensity;
    public GameObject wall;
    
    
    private int[,] grid;
    private bool[,] vistedBefore;
    private List<Vector2> finalPath;
    private bool hasStart;
    private bool hasEnd;
    private int ballout;
    private Vector2 startPos;
    private Vector2 endPos;
    private bool hasPos;
    private Vector2 lastSpot;
    

    // Start is called before the first frame update
    void Start()
    {
        hasPos = false;
        hasEnd = false;
        hasStart = false;
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
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                grid[i, j] = number(invDensity);
                if (grid[i, j] == 1)
                {
                    Instantiate(wall, new Vector3(i, 0, j), Quaternion.identity);
                }
            }
        }
        while (!hasStart)
        {
            startPos = FindRandomEmptyLocation();
            hasStart = true;
            start.transform.position = new Vector3(startPos.x, 1, startPos.y);
            vistedBefore[(int) startPos.x, (int) startPos.y] = true;
            finalPath.Add(new Vector2(currentPosition.x, currentPosition.y));
        }
        
        while (!hasEnd)
        {
            endPos = FindRandomEmptyLocation();
            if (startPos != endPos)
            {
                hasEnd = true;
                end.transform.position = new Vector3(endPos.x,1,endPos.y);
            }
        }
    }

    private void Update()
    {
        FindPath();
        if (Input.GetKeyDown("w"))
        {
            startPos = new Vector2(startPos.x,startPos.y+1);
            Reset();
        }
        if (Input.GetKeyDown("s"))
        {
            startPos = new Vector2(startPos.x,startPos.y-1);
            Reset();
        }       
        if (Input.GetKeyDown("a"))
        {
            startPos = new Vector2(startPos.x-1,startPos.y);
            Reset();
        }        
        if (Input.GetKeyDown("d"))
        {
            startPos = new Vector2(startPos.x+1,startPos.y);
            Reset();
        }
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


    int number(int max)
    {
        int i = Random.Range(0, max);
        return i;
    }

    private Vector2 FindRandomEmptyLocation()
    {
        bool keeplooking = true;
        while (keeplooking)
        {
            int h = number(height);
            int w = number(width);

            if (grid[w, h] == 0)
            {
                return new Vector2(w, h);
            }
        }
        return new Vector2(11, 11);
    }

    private void FindPath()
    {
        ballout = 0;
        if (!hasPos)
        {
            currentPosition = startPos;
            hasPos = true;
        }
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
        start.transform.position = new Vector3(startPos.x, 1, startPos.y);
        
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
}