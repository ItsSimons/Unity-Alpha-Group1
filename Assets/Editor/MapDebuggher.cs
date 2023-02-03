using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGen))]
public class MapDebuggher : Editor
{
    public override void OnInspectorGUI()
    {
        MapGen map_gen = (MapGen)target;

        if (DrawDefaultInspector())
        {
            if (map_gen.auto_update)
            {
                map_gen.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            map_gen.GenerateMap();
        }
    }
}
