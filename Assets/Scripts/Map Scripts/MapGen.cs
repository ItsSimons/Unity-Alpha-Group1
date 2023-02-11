using System.Collections;
using System.Collections.Generic;
using AStarSharp;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MapGen : MonoBehaviour
{
    //Map size
    public int map_width = 100;
    public int map_height = 100;
    //"zoom" level of the generated map
    public float map_scale = 2.71f;
    //noise "filter" higher value means more is getting cut off
    public float map_filter = 0.80f;
    
    //Number of octaves of iteration of the noise, more is more smooth, do not use more than 10.
    public int octaves = 5;
    //Value 0 to 1, how quickly the amplitudes diminish for each octave
    public float persistance = 0.5f;
    //Determines how quickly the frequency increases for each octave
    public float lacunarity = 1.5f;
    //Octaves offset
    public Vector2 offset;
    
    //River nodes
    public int river_nodes = 11;
    //Offset bewteen nodes
    public int river_offset = 3;

    //Resurrect stations
    public int resurrect_stations = 4;
    
    //Secondary rivers
    public bool side_rivers_toggle = false;
    public int side_rivers = 2;
    public int side_river_nodes = 5;
    
    //Map has been generated?
    private bool success = true;
    //Shared ressurect stations sed
    [SerializeField] private int res_seed = 0;

    private void Awake()
    {
        RefreshSeed();
    }

    public void RefreshSeed()
    {
        res_seed = UnityEngine.Random.Range(0, 100000);

    }

    /// <summary>
    /// Same output but as vector3 list
    /// </summary>
    /// <returns></returns>
    public List<Vector3Int> GenerateMapAsVectors()
    {
        var terrain = GenerateMap();

        List<Vector3Int> terrain_vector = new List<Vector3Int>();

        for (int x = 0; x < terrain.GetLength(0); x++)
        {
            for (int y = 0; y < terrain.GetLength(1); y++)
            {
                if(terrain[x,y] != 0)
                {
                    terrain_vector.Add(new Vector3Int(x,y,terrain[x,y]));
                }
            }
        }

        return terrain_vector;
    }
    
    //This is a stupid fix but works for the limited timeframe
    //Returns a 2D array containing terrain values, 0 is void, 1 is rock, 2 is river
    public int[,] GenerateMap()
    {
        int[,] terrain = null;
        int seed = 0;
        
        //Generates the map until a valid one is given
        do
        {
            seed = UnityEngine.Random.Range(0, 1000000);
            terrain = GenerateTerrain(seed);
        } while (terrain == null);

        return terrain;
    }

    private int[,] GenerateTerrain(int seed)
    {
        //defaults at true if the map successfully generated
        success = true;
        
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
        
        //Populates all the values of the pathfinding map
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                grid[x][y] = new Node(new System.Numerics.Vector2(x,y),true,1f);
            }
        }
        
        var river_path = GenerateRivers(grid, prng, 2);

        //Tile value for the river section is extracted
        if (river_path != null)
        {
            foreach (var node in river_path)
            {
                int x_node = (int)node.Position.X;
                int y_node = (int)node.Position.Y;

                //River nodes gets added to terrain map
                terrain_map[x_node, y_node] = 2;
                
                //This is definitely the worst way to thicken up rivers, but it is a fast fix for the short ammount of
                //time available for development 
                if (x_node > 1 && x_node < map_width - 2)
                {
                    terrain_map[x_node - 1, y_node] = 2;
                    terrain_map[x_node + 1, y_node] = 2;
                    terrain_map[x_node - 2, y_node] = 2;
                    terrain_map[x_node + 2, y_node] = 2;
                }
                if (y_node > 1 && y_node < map_height - 2)
                {
                    terrain_map[x_node, y_node - 1] = 2;
                    terrain_map[x_node, y_node + 1] = 2;
                    terrain_map[x_node, y_node - 2] = 2;
                    terrain_map[x_node, y_node + 2] = 2;
                }
            }

            //River border to the generated map
            for (int x = 0; x < map_width; x++)
            {
                terrain_map[x, 0] = 2;
                terrain_map[x, map_height - 1] = 2;
            }
            for (int y = 0; y < map_height; y++)
            {
                terrain_map[0, y] = 2;
                terrain_map[map_width - 1, y] = 2;
            }
            
            //Generates 4 resurrect stations
            prng = new System.Random(res_seed);
            
            for (int i = 0; i < resurrect_stations; i++)
            {
                int value_x = prng.Next(10, map_width - 10);
                int value_y = prng.Next(10, map_height - 10);

                for (int y = 1; y < 4; y++)
                {
                    for (int x = 1; x < 4; x++)
                    {
                        terrain_map[value_x + x, value_y + y] = 3;
                    }
                }
            }
        }
        else
        {
            success = false;
        }

        //Not so nice invalid-seed fix.
        if (success == true)
        {
            //0 is void
            //1 is rock
            //2 is river
            //3 is res station
            return terrain_map;
        }
        else
        {
            return null;
        }
    }

    public Stack<Node> GenerateRivers(List<List<Node>> grid, System.Random prng, int n_of_rivers)
    {
        //initiates the pathfind algorithm with the grid provided
        Astar pathfind = new Astar(grid);
        Stack<Node> river_path = new Stack<Node>();
        river_path.Clear();

        List<System.Numerics.Vector2> all_river_points = new List<System.Numerics.Vector2>();
        
        for (int j = 0; j < n_of_rivers; j++)
        {
            //Collection of points where rivers will generate from, first one is created randomly starting in the Y axis 
            System.Numerics.Vector2[] river_points = new System.Numerics.Vector2[river_nodes];
            river_points[0] = new System.Numerics.Vector2(0, prng.Next(map_height - 15));
            int river_points_n = river_points.GetLength(0);

            int section = map_width / (river_points_n - 2);
            int value_x = 0;
            int value_y = 0;
        
            //creates all the different river point at a slight offset between each other
            for (int i = 1; i < river_points_n - 1; i++)
            {
                value_x = section * i;

                do
                { 
                    value_y = prng.Next((int)river_points[i - 1].Y - river_offset, (int)river_points[i - 1].Y + river_offset);
                } 
                while (value_y < 0 && value_y > map_height);

                river_points[i] = new System.Numerics.Vector2(value_x, value_y);
            }
            //last point has to be manually set, will be at opposite side of the map
            value_x = map_width - 1;
            value_y = prng.Next((int)river_points[river_points_n - 2].Y - river_offset, 
                (int)river_points[river_points_n - 2].Y + river_offset);
            river_points[river_points_n - 1] = new System.Numerics.Vector2(value_x, value_y);

            //50% of river being mirrored, because why not!
            if (prng.Next(0, 20) > 10)
            {
                for (int i = 0; i < river_points.GetLength(0); i++)
                {
                    var point = river_points[i];
                    river_points[i] = new System.Numerics.Vector2(point.Y, point.X);
                }
            }

            //path from a river point to the next
            for (int i = 0; i < river_points_n - 1; i++)
            {
                var temp_path = pathfind.FindPath(river_points[i], river_points[i+1]);

                //if path is not valid fails the map generation
                if (temp_path == null)
                {
                    success = false;
                    break;
                }

                //if it is valid, adds rivers nodes to final river path
                foreach (var node in temp_path)
                {
                    river_path.Push(node);
                }
            }

            //Adds river points found inside all river points
            foreach (var point in river_points)
            {
                all_river_points.Add(point);
            }
        }

        //Side rivers are unfinished, are probably not gonna be finished. do NOT turn on
        if (side_rivers_toggle)
        {
            //Here generates littler rivers starting from the main one to the side of the map
            int used_values = 0;
            int start_river_point = 0;
            int all_river_points_n = all_river_points.Count;

            for (int i = 0; i < side_rivers; i++)
            {
                int side = prng.Next(0, 2);
                do { start_river_point = prng.Next(1, all_river_points_n - 1); } while (used_values == start_river_point);
                used_values = start_river_point;

                System.Numerics.Vector2 side_river_start = all_river_points[start_river_point];
                System.Numerics.Vector2 side_river_end = new System.Numerics.Vector2(all_river_points[start_river_point].X, (map_height - 1) * side);
            
                var temp_path = pathfind.FindPath(side_river_start, side_river_end);

                //if path is not valid fails the map generation
                if (temp_path == null)
                {
                    success = false;
                    break;
                }

                //if it is valid, adds rivers nodes to final river path
                foreach (var node in temp_path)
                {
                    river_path.Push(node);
                }
            }
        }

        return river_path;
    }
}
