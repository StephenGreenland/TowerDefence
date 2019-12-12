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
    public Text endTimer;
    public Click clickmanager;
    
    public float timer;
    private int waveLevel;
    private int numberOfSpawns;
    private int roundNumber;

    public float timeTillEnd;

    // Start is called before the first frame update
    void Start()
    {
        waveLevel = 0;
        timer = 20f;
        numberOfSpawns = 5;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer -= Time.deltaTime;
        timeTillEnd -= Time.deltaTime;
         if(timer < 0)
         {
            spawnWave();
            timer = 20f;
            clickmanager.money += 4;
        }
        uiTimer.text =  ("Time till next wave ") + Mathf.Round(timer).ToString();
        endTimer.text = ("Time to end of Level ") + Mathf.Round(timeTillEnd).ToString();
    }

    void spawnWave()
    {
        for (int i = 0; i < numberOfSpawns+waveLevel; i++)
        {
            Instantiate(basicUnit, Vector3.zero, Quaternion.identity);
            
        }
       
        waveLevel++;
        
        
    }
    Vector3 GetSpawnPoint()
    {
        int i;
        i = Random.Range(0, spawnPoints.Count-1);
        return new Vector3(spawnPoints[i].x, 0, spawnPoints[i].y);

    }


}
