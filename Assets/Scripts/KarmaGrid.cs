using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaGrid : MonoBehaviour
{
    public int width;
    public int height;
    public float tile_size;
    public float karma_colour_scaling;
    public int[,] karma;
    public GameObject[,] planes;
    public Vector3 corner_pos;
    public bool map_generated;

    void Start()
    {
        karma = new int[width,height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                karma[i,j] = 0;
            }
        }
        planes = new GameObject[width, height];
        generateMap();
        hideMap();
    }

    void Update()
    {
        if (Input.GetKeyDown("h") && !map_generated)
        {
            generateMap();
        }
        else if (Input.GetKeyDown("j"))
        {
            removeMap();
        }
        else if (Input.GetKeyDown("u"))
        {
            updateMap();
        }
        else if (Input.GetKeyDown("n"))
        {
            showMap();
        }
        else if (Input.GetKeyDown("m"))
        {
            hideMap();
        }
        else if (Input.GetKeyDown("r"))
        {
            randomGen();
        }
    }

    void randomGen()
    {
        karmaChange(Random.Range(0, width),Random.Range(0,height), Random.Range(-1,2) * 50);
    }

    // Used to change karma in a SINGLE POINT affecting 4 NEIGHBOURING TILES
    void karmaChange(int pos_x, int pos_y, int strength)
    {
        karma[pos_x, pos_y] += strength;
        if (pos_x > 0)
        {
            karma[pos_x-1,pos_y] += strength;
        }
        if (pos_x < width - 1)
        {
            karma[pos_x+1,pos_y] += strength;
        }
        if (pos_y > 0)
        {
            karma[pos_x,pos_y-1] += strength;
        }
        if (pos_y < height - 1)
        {
            karma[pos_x,pos_y+1] += strength;
        }
        updateMap();
    }
    // Use on creation, upgrade and demonition of buildings/structures etc.

    void generateMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
                planes[i,j] = obj;
                obj.transform.position = new Vector3(corner_pos.x + (i * tile_size * 10) + (5 * tile_size), corner_pos.y + 0.05f, corner_pos.z + (j * tile_size * 10) + (5 * tile_size));
                obj.transform.localScale = new Vector3(tile_size * 0.9f, tile_size * 0.9f, tile_size * 0.9f);
                obj.transform.SetParent(this.transform);
                obj.tag = "Karma";
                float blue = 255 - Mathf.Abs(karma[i,j]) * karma_colour_scaling; 
                float green = 255 - Mathf.Abs(karma[i,j]) * karma_colour_scaling;
                float red = 255 - Mathf.Abs(karma[i,j]) * karma_colour_scaling;
                if (karma[i,j] > 0)
                {
                    green = 255;
                }
                else if (karma[i,j] < 0)
                {
                    red = 255;
                }
                obj.GetComponent<Renderer>().material.color = new Color32((byte)red, (byte)green, (byte)blue, 150);
            }
        }
        map_generated = true;
    }

    void updateMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float blue = 255 - Mathf.Abs(karma[i,j]) * karma_colour_scaling; 
                float green = 255 - Mathf.Abs(karma[i,j]) * karma_colour_scaling;
                float red = 255 - Mathf.Abs(karma[i,j]) * karma_colour_scaling;
                if (karma[i,j] > 0)
                {
                    green = 255;
                }
                else if (karma[i,j] < 0)
                {
                    red = 255;
                }
                planes[i,j].GetComponent<Renderer>().material.color = new Color32((byte)red, (byte)green, (byte)blue, 255);
            }
        }
    }

    void showMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                planes[i, j].SetActive(true);
            }
        }
    }

    void hideMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                planes[i, j].SetActive(false);
            }
        }
    }

    void removeMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Destroy(planes[i, j]);
            }
        }
        map_generated = false;
    }
}
