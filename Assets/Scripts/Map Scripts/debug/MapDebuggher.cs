using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//This is just so that the map gen can be tested in-editor.
[CustomEditor(typeof(MapGen))]
public class MapDebuggher : Editor
{
    public override void OnInspectorGUI()
    {
        MapGen map_gen = (MapGen)target;

        if (DrawDefaultInspector())
        {
            MapRender renderer = FindObjectOfType<MapRender>();
            var map = map_gen.GenerateMap();
            if (map != null)
            {
                renderer.RenderNoiseMap(map);
            }
        }

        if (GUILayout.Button("Generate"))
        {
            //Render the map on the plane
            MapRender renderer = FindObjectOfType<MapRender>();
            var map = map_gen.GenerateMap();
            if (map != null)
            {
                renderer.RenderNoiseMap(map);
            }
        }
        
        if (GUILayout.Button("Test"))
        {
            var map = map_gen.GenerateMap();

            foreach (var node in map)
            {
                Debug.Log(node);
            }
        }
    }
}
