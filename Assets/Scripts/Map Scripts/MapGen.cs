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
    
    //River nodes
    public int river_nodes = 6;
    
    //This is here temporarily to test the noise
    public bool auto_update;
    
    public void GenerateMap()
    {
        System.Random prng = new System.Random(seed);
        
        float[,] noise_map = PerlinNoise.GenerateNoiseMap(map_width, map_height, prng, octaves, map_scale, persistance, lacunarity, offset);
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

        System.Numerics.Vector2[] river_points = new System.Numerics.Vector2[river_nodes];
        
        river_points[0] = new System.Numerics.Vector2(0, prng.Next(map_height - 1));

        int river_points_n = river_points.GetLength(0);

        int value_x = 0;
        int value_y = 0;
        
        for (int i = 1; i < river_points_n - 1; i++)
        {
            value_x = (map_width / river_points_n) * i;
            value_y = prng.Next((int)river_points[0].Y, (int)river_points[0].Y + 10);
            
            river_points[i] = new System.Numerics.Vector2(value_x, value_y);
        }
        
        value_x = map_width - 1;
        value_y = prng.Next((int)river_points[0].Y, (int)river_points[0].Y + 10);
        river_points[river_points.GetLength(0) - 1] = new System.Numerics.Vector2(value_x, value_y);
      

        //initiates the pathfind algorithm with the grid provided
        Astar pathfind = new Astar(grid);
        Stack<Node> path = new Stack<Node>();
        path.Clear();

        for (int i = 0; i < river_points_n - 1; i++)
        {
            var temp_path = pathfind.FindPath(river_points[i], river_points[i+1]);

            foreach (var node in temp_path)
            {
                path.Push(node);
            }
        }

        //Tile value for the river section is extracted
        if (path != null)
        {
            foreach (var node in path)
            {
                int x_node = (int)node.Position.X;
                int y_node = (int)node.Position.Y;

                terrain_map[x_node, y_node] = 2;
            }
        }

        //Render the map on the plane
        MapRender renderer = FindObjectOfType<MapRender>();
        renderer.RenderNoiseMap(terrain_map);
    }
}
