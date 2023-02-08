using System.Collections;
using System.Collections.Generic;
using AStarSharp;
using Unity.VisualScripting;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int map_width;
    public int map_height;
    public float map_scale;
    public float map_filter;

    public int octaves;
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;
    
    public bool auto_update;
    
    public void GenerateMap()
    {
        float[,] noise_map = PerlinNoise.GenerateNoiseMap(map_width, map_height, seed,octaves, map_scale, persistance, lacunarity, offset);
        int[,] noise_map_int = new int[map_width,map_height];
        
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                if (noise_map[x, y] > map_filter)
                {
                    noise_map_int[x, y] = 1;
                }
                else
                {
                    noise_map_int[x, y] = 0;
                }
            }
        }
        
        MapRender display = FindObjectOfType<MapRender>();

        
        List<List<Node>> grid = new List<List<Node>>();

        for (int y = 0; y < map_height; y++)
        {
            grid.Add(new List<Node>());

            for (int x = 0; x < map_width; x++)
            {
                grid[y].Add(null);
            }
        }
        
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                if (noise_map_int[x, y] == 1)
                {
                    grid[x][y] = new Node(new System.Numerics.Vector2(x,y),false,1f);
                }
                else if (noise_map_int[x, y] == 0)
                {
                    grid[x][y] = new Node(new System.Numerics.Vector2(x,y),true,1f);
                }
            }
        }

        Astar pathfind = new Astar(grid);

        var path = pathfind.FindPath(new System.Numerics.Vector2(1, 35), new System.Numerics.Vector2(49, 30));

        foreach (var node in path)
        {
            int x_node = (int)node.Position.X;
            int y_node = (int)node.Position.Y;
            
            noise_map_int[x_node, y_node] = 2;
            
            Debug.Log(node.Position);
        }
        
        display.RenderNoiseMap(noise_map_int);
    }
}
