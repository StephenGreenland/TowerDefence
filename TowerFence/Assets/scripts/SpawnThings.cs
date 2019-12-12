using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnThings : MonoBehaviour
{
    public GameObject basicUnit;
    public PathFinder grid;
    public List<Vector2> spawnPoints;
    public Text uiTimer;
    
    public Click clickmanager;
    
    public float timer;
    private int waveLevel;
    private int numberOfSpawns;
    private int roundNumber;

    public Vector2[] path;
    
    
    public enum  myEnum
    {
        purple,red
    }

    private GameObject castle;
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
        
        waveLevel = 0;
        numberOfSpawns = 5;

       // path = grid.FindPath(new Vector2((int) Mathf.Round(transform.position.x), (int) Mathf.Round(transform.position.z)),
                 //   new Vector2((int) Mathf.Round(transform.position.x), (int) Mathf.Round(transform.position.x)));

    }

    // Update is called once per frame
    void Update()
    {
        
        
        timer = timer -= Time.deltaTime;
        
         if(timer < 0)
         {
            spawnWave();
            timer = 20f;
            clickmanager.money += 4;
        }
        uiTimer.text =  ("Time till next wave ") + Mathf.Round(timer).ToString();
        
    }

    void spawnWave()
    {
        StartCoroutine(Wait());

       
        waveLevel++;
        
        
    }
    Vector3 GetSpawnPoint()
    {
        int i;
        i = Random.Range(0, spawnPoints.Count-1);
        return new Vector3(spawnPoints[i].x, 0, spawnPoints[i].y);

    }

    private IEnumerator Wait()
    {
        for (int i = 0; i < numberOfSpawns+waveLevel; i++)
        {
            yield return  new WaitForSeconds(1);
            Instantiate(basicUnit, new Vector3(transform.position.x,1, transform.position.z), Quaternion.identity);
            
           
        }

    }
}
