using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(scanner))]
public class scannerbutton : Editor
{
        public override void OnInspectorGUI()
        {
                base.OnInspectorGUI();
                {
                        //DrawDefaultInspector();
                        scanner myscript = (scanner) target;
                        if (GUILayout.Button("Scan da world"))
                        {
                                myscript.Scan();
                        }
                }
        }
}
