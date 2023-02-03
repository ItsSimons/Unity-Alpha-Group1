using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int map_width;
    public int map_height;
    public float map_scale;

    public bool auto_update;
    
    public void GenerateMap()
    {
        float[,] noise_map = PerlinNoise.GenerateNoiseMap(map_width, map_height, map_scale);

        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                if (noise_map[x, y] > 0.8f)
                {
                    noise_map[x, y] = 1;
                }
                else
                {
                    noise_map[x, y] = 0;
                }
            }
        }
        
        MapRender display = FindObjectOfType<MapRender>();
        
        display.RenderNoiseMap(noise_map);
    }
}
