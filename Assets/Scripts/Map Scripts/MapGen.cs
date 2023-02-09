using System.Collections;
using System.Collections.Generic;
using AStarSharp;
using Unity.VisualScripting;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    //Map size
    public int map_width;
    public int map_height;
    //"zoom" level of the generated map
    public float map_scale;
    //noise "filter" higher value means more is getting cut off
    public float map_filter;
    
    //Number of octaves of iteration of the noise, more is more smooth, do not use more than 10.
    public int octaves;
    //Value 0 to 1, how quickly the amplitudes diminish for each octave
    public float persistance;
    //Determines how quickly the frequency increases for each octave
    public float lacunarity;

    //Similar to a minecraft seed, value will determine the end result
    public int seed;
    public Vector2 offset;
    
    //This is here temporarily to test the noise
    public bool auto_update;
    
    public void GenerateMap()
    {
        float[,] noise_map = PerlinNoise.GenerateNoiseMap(map_width, map_height, seed,octaves, map_scale, persistance, lacunarity, offset);
        int[,] terrain_map = new int[map_width,map_height];
        
        //From the noise map cuts of the value at filter value.
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                if (noise_map[x, y] > map_filter)
                {
                    terrain_map[x, y] = 1;
                }
                else
                {
                    terrain_map[x, y] = 0;
                }
            }
        }
        
        //Inits a pathfinding grid, where the values form the previously used noise will be vied as obstacles
        List<List<Node>> grid = new List<List<Node>>();
        for (int y = 0; y < map_height; y++)
        {
            //Appends a list to the list for each Y to mimic a 2D list
            grid.Add(new List<Node>());
            
            for (int x = 0; x < map_width; x++)
            {
                //Initializes all the elements in the second list with null, as it will be populated later 
                grid[y].Add(null);
            }
        }
        
        //Populates all the values of the pathfinding map, if an obstacle is found at a specific coordinate
        //that node will now be set as non-walkable
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                if (terrain_map[x, y] == 1)
                {
                    //obstacle
                    grid[x][y] = new Node(new System.Numerics.Vector2(x,y),false,1f);
                }
                else if (terrain_map[x, y] == 0)
                {
                    //not obstacle
                    grid[x][y] = new Node(new System.Numerics.Vector2(x,y),true,1f);
                }
            }
        }

        //initiates the pathfind algorithm with the grid provided
        Astar pathfind = new Astar(grid);
        //Pathfinds trough the obstacles to generate a river
        var path = pathfind.FindPath(new System.Numerics.Vector2(1, 35), new System.Numerics.Vector2(49, 30));

        //Tile value for the river section is extracted
        foreach (var node in path)
        {
            int x_node = (int)node.Position.X;
            int y_node = (int)node.Position.Y;
            
            terrain_map[x_node, y_node] = 2;
        }
        
        //Render the map on the plane
        MapRender renderer = FindObjectOfType<MapRender>();
        renderer.RenderNoiseMap(terrain_map);
    }
}
