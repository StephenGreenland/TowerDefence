using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Castle : MonoBehaviour
{
    public GameObject scenelose;
    public Health health;
    public enum Colour
    {
        Purple, Red
    }

    public Colour myColour;
    
    // Start is called before the first frame update
    void Start()
    {
        health.OutOfHealth += Lose;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyBase>() != null)
        {
            health.Change(-1);
            
        }
        Destroy(other.gameObject);

    }

    public void Lose()
    {
        scenelose.GetComponent<SceneChanger>().ChangeScene();


    }

    private void OnDestroy()
    {
        health.OutOfHealth -= Lose;
    }
}
