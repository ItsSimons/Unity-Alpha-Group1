using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VibesGrid : MonoBehaviour
{
    [SerializeField] public int width;
    [SerializeField] public int height;
    [SerializeField] private float karma_colour_scaling;
    [SerializeField] public int[,] vibes;

    [SerializeField] private Tilemap tilemap;

    // Create 2D array for vibes
    void Start()
    {
        vibes = new int[width,height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                vibes[i,j] = 0;
            }
        }
        hideMap();
    }

    // Test inputs for showing and randomizing map
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            showMap();
        }
        else if (Input.GetKeyDown("m"))
        {
            hideMap();
        }
        else if (Input.GetKeyDown("r"))
        {
            randomGen3x3();
            randomGen3x3();
            randomGen3x3();
            randomGen3x3();
            randomGen3x3();
            randomGen3x3();
        }
    }

    // ----------------------------
    // Debug random vibe generation
    void randomGen()
    {
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);
        int str = Random.Range(-1, 2);
        karmaChange1x1(x, y, str);
    }
    
    void randomGen2x2()
    {
        int x = Random.Range(0, width-1);
        int y = Random.Range(1, height);
        int str = Random.Range(-1, 2);
        karmaChange2x2(x, y, str);
    }
    
    void randomGen3x3()
    {
        int x = Random.Range(0, width-2);
        int y = Random.Range(2, height);
        int str = Random.Range(-1, 2);
        karmaChange3x3(x, y, str);
    }
    // ----------------------------

    // Used to change karma in a SINGLE POINT affecting 4 NEIGHBOURING TILES
    public void karmaChange1x1(int pos_x, int pos_y, int strength)
    {
        vibes[pos_x, pos_y] += strength;
        if (pos_x > 0)
        {
            vibes[pos_x-1,pos_y] += strength;
        }
        if (pos_x < width - 1)
        {
            vibes[pos_x+1,pos_y] += strength;
        }
        if (pos_y > 0)
        {
            vibes[pos_x,pos_y-1] += strength;
        }
        if (pos_y < height - 1)
        {
            vibes[pos_x,pos_y+1] += strength;
        }
        updateMap();
    }
    // Use on creation, upgrade and demonition of buildings/structures etc.
    
    // Pos x and y is FAR LEFT POINT OF THE 2x2 [ between 0,100 and 99,1 ]
    // Used to change karma in a 2x2 AREA affecting 8 NEIGHBOURING TILES
    public void karmaChange2x2(int pos_x, int pos_y, int strength)
    {
        vibes[pos_x, pos_y] += strength;
        vibes[pos_x, pos_y-1] += strength;
        vibes[pos_x+1, pos_y] += strength;
        vibes[pos_x+1, pos_y-1] += strength;
        if (pos_x > 0)
        {
            vibes[pos_x-1,pos_y] += strength;
            vibes[pos_x-1,pos_y-1] += strength;
        }
        if (pos_x+1 < width - 1)
        {
            vibes[pos_x+2,pos_y] += strength;
            vibes[pos_x+2,pos_y-1] += strength;
        }
        if (pos_y -1 > 0)
        {
            vibes[pos_x,pos_y-2] += strength;
            vibes[pos_x+1,pos_y-2] += strength;
        }
        if (pos_y < height - 1)
        {
            vibes[pos_x,pos_y+1] += strength;
            vibes[pos_x+1,pos_y+1] += strength;
        }
        updateMap();
    }
    // Use on creation, upgrade and demonition of buildings/structures etc.
    
    // Pos x and y is FAR LEFT POINT OF THE 3x3 [ between 0,100 and 98,2 ]
    // Used to change karma in a 3x3 AREA affecting 8 NEIGHBOURING TILES
    public void karmaChange3x3(int pos_x, int pos_y, int strength)
    {
        vibes[pos_x, pos_y] += strength;
        vibes[pos_x, pos_y-1] += strength;
        vibes[pos_x, pos_y-2] += strength;
        vibes[pos_x+1, pos_y] += strength;
        vibes[pos_x+1, pos_y-1] += strength;
        vibes[pos_x+1, pos_y-2] += strength;
        vibes[pos_x+2, pos_y] += strength;
        vibes[pos_x+2, pos_y-1] += strength;
        vibes[pos_x+2, pos_y-2] += strength;
        if (pos_x > 0)
        {
            vibes[pos_x-1,pos_y] += strength;
            vibes[pos_x-1,pos_y-1] += strength;
            vibes[pos_x-1,pos_y-2] += strength;
        }
        if (pos_x+2 < width - 1)
        {
            vibes[pos_x+3,pos_y] += strength;
            vibes[pos_x+3,pos_y-1] += strength;
            vibes[pos_x+3,pos_y-2] += strength;
        }
        if (pos_y -2 > 0)
        {
            vibes[pos_x,pos_y-3] += strength;
            vibes[pos_x+1,pos_y-3] += strength;
            vibes[pos_x+2,pos_y-3] += strength;
        }
        if (pos_y < height - 1)
        {
            vibes[pos_x,pos_y+1] += strength;
            vibes[pos_x+1,pos_y+1] += strength;
            vibes[pos_x+2,pos_y+1] += strength;
        }
        updateMap();
    }
    // Use on creation, upgrade and demonition of buildings/structures etc.

    // Updates colour of map based on vibes
    void updateMap()
    {
        Color32 new_col = new Color32(255,255,255,255);
        float r;
        float g;
        float b;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                r = 255 - Mathf.Abs(vibes[i,j]) * karma_colour_scaling; 
                g = 255 - Mathf.Abs(vibes[i,j]) * karma_colour_scaling;
                b = 255 - Mathf.Abs(vibes[i,j]) * karma_colour_scaling;

                if (vibes[i,j] > 0)
                {
                    g = 255;
                }
                else if (vibes[i,j] < 0)
                {
                    r = 255;
                }
                new_col = new Color32((byte)r, (byte)g, (byte)b, 255);
                ChangeTileColour(new_col, new Vector3Int(i - 50, j - 50, 0), tilemap);
            }
        }
    }

    // Change tile colour of Vec3 position to a new colour on existing tilemap
    void ChangeTileColour(Color32 col, Vector3Int pos, Tilemap tilemap)
    {
        tilemap.SetTileFlags(pos, TileFlags.None);
        tilemap.SetColor(pos, col);
    }

    // Reveal Vibe Map
    void showMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tilemap.gameObject.SetActive(true);
            }
        }
    }

    // Hide Vibe Map
    void hideMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tilemap.gameObject.SetActive(false);
            }
        }
    }
}
