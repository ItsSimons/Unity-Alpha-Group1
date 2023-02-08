using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Class generates a perlin noise map using a X, Y and a scale
public static class PerlinNoise
{
    public static float[,] GenerateNoiseMap(int map_width, int map_height, int seed, int octaves, float scale, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noise_map = new float[map_width, map_height];

        System.Random prng = new System.Random(seed);
        Vector2[] octave_offset = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offset_x = prng.Next(-100000, 100000) + offset.x;
            float offset_y = prng.Next(-100000, 100000) + offset.y;
            octave_offset[i] = new Vector2(offset_x, offset_y);
        }
        
        //If scale provided is less or zero apply a base not-null scale
        if (scale <= 0)
        {
            scale = 0.001f;
        }

        float max_noise_height = float.MinValue;
        float min_noise_height = float.MaxValue;

        float half_width = map_width / 2f;
        float half_height = map_height / 2f;

        //Generates a heat map using perlin noise
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noise_height = 0;

                //Octaves loop
                for (int i = 0; i < octaves; i++)
                {
                    float scaled_x = (x-half_width) / scale * frequency + octave_offset[i].x;
                    float scaled_y = (y-half_height) / scale * frequency - octave_offset[i].y;

                    float perlin_value = Mathf.PerlinNoise(scaled_x, scaled_y) * 2 - 1;
                    noise_height += perlin_value * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if(noise_height > max_noise_height)
                {
                    max_noise_height = noise_height;
                }
                else if (noise_height < min_noise_height)
                {
                    min_noise_height = noise_height;
                }
                
                noise_map[x, y] = noise_height;
            }
        }

        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                noise_map[x, y] = Mathf.InverseLerp(min_noise_height, max_noise_height, noise_map[x, y]);
            }
        }

        return noise_map;
    }
}
