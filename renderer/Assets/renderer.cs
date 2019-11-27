using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rendererer : MonoBehaviour
{
    private byte[] backBuffer;
    private int Xsize;
    private int Ysize;
    
    private Texture2D tex;
    private Renderer quadrederer;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        Xsize = 16;
        Ysize = 16;
        
            
        
        tex = new Texture2D(Xsize,Ysize,TextureFormat.RGB24,false);

        quadrederer.material.mainTexture = tex;
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
