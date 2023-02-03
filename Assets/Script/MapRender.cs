using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRender : MonoBehaviour
{
    public Renderer texture_render;

    public void RenderNoiseMap(float[,] noise_map)
    {
        int width = noise_map.GetLength(0);
        int height = noise_map.GetLength(1);

        Texture2D map_texture = new Texture2D(width, height);

        Color[] color_map = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                color_map[y * width + x] = Color.Lerp(Color.black, Color.white, noise_map[x, y]);
            }
        }
        
        map_texture.SetPixels(color_map);
        map_texture.Apply();

        texture_render.sharedMaterial.mainTexture = map_texture;
        texture_render.transform.localScale = new Vector3(width, 1, height);
    }
}
