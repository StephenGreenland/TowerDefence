using System.Collections;
 using System.Collections.Generic;
 using System.Runtime.InteropServices;
 using UnityEngine;
 
 namespace Stephen
 {
 
 
 
     public class Renderer : MonoBehaviour
     {
         private byte[,] backBuffer;
         public Texture quadRenderer;
 
         // Start is called before the first frame update
         void Start()
         {
             backBuffer = new byte[16,16];
             
             Texture2D tex = new Texture2D(16, 16, TextureFormat.RGB24, false);
             tex.filterMode = FilterMode.Point; // This turns off blur for debugging
           //  quadRenderer.Material.mainTexture = tex;
 
 
         }
 
         // Update is called once per frame
         void Update()
         {
 
         }
     }
 }