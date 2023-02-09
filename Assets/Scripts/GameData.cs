using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int currency = 0;
    public int dailyCost = 0;
    public float time;

    public int souls_heaven_Green;
    public int souls_heaven_Yellow;
    public int souls_heaven_Orange;
    public int souls_heaven_Brown;
    public int souls_heaven_Purple;
    public int souls_heaven_Red;
    public int souls_heaven_Blue;

    public int souls_hell;

    public int angels;
    public int demons;

    void Start()
    {
        currency = 1000;
        float time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= time + 5f)
        {
            currency -= dailyCost;
            time = Time.time;
            //currencyAmount += 10;
        }
    }
}
