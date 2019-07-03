using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MapGenerator script = (MapGenerator)target;
        if (GUILayout.Button("Generate Map"))
        {
            script.GenerateMap();
        }

        //base.OnInspectorGUI();

    }


}
