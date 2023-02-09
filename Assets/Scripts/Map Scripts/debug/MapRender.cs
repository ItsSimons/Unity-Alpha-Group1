using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a visualizer that allows the mapGen to show something on screen
public class MapRender : MonoBehaviour
{
    public Renderer texture_render;

    //generates a heatmap of the raw heath value of the noise if a Float is given
    public void RenderNoiseMap(float[,] noise_map)
    {
        int width = noise_map.GetLength(0);
        int height = noise_map.GetLength(1);

        Texture2D map_texture = new Texture2D(width, height);
        Color[] color_map = new Color[width * height];

        //For each value in the noise makes it completely black if the value is 1 and back if 0, considering all the 
        //shades in-betweem
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                color_map[y * width + x] = Color.Lerp(Color.black, Color.white, noise_map[x, y]);
            }
        }
        
        ApplyTexture(map_texture, color_map, width, height);
    }

    public void RenderNoiseMap(int[,] noise_map)
    {
        int width = noise_map.GetLength(0);
        int height = noise_map.GetLength(1);

        Texture2D map_texture = new Texture2D(width, height);
        Color[] color_map = new Color[width * height];

        //for each value in the INT noise map generates based on the ID respectively a rock, river or nothing
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                switch (noise_map[x,y])
                {
                    case 1:
                        //Rock
                        color_map[y * width + x] = Color.red;
                        break;
                    case 2:
                        //River
                        color_map[y * width + x] = Color.green;
                        break;
                    default:
                        //nothing
                        color_map[y * width + x] = Color.black;
                        break;
                }
            }
        }
        
        ApplyTexture(map_texture, color_map, width, height);
    }
    
    private void ApplyTexture(Texture2D map_texture, Color[] color_map, int width, int height)
    {
        //Applies color map on the texture
        map_texture.SetPixels(color_map);
        map_texture.Apply();
        texture_render.sharedMaterial.mainTexture = map_texture;
        texture_render.transform.localScale = new Vector3(width, 1, height);
    }
}
