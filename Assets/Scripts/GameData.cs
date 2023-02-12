using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public float currency = 0;

    public int souls_heaven_Green;
    public int souls_heaven_Yellow;
    public int souls_heaven_Orange;
    public int souls_heaven_Brown;
    public int souls_heaven_Purple;
    public int souls_heaven_Red;
    public int souls_heaven_Blue;

    public int souls_hell_Green;
    public int souls_hell_Yellow;
    public int souls_hell_Orange;
    public int souls_hell_Brown;
    public int souls_hell_Purple;
    public int souls_hell_Red;
    public int souls_hell_Blue;

    public int souls_total;

    public int angels;
    public int demons;
}

// Don't put functions here, GameData is a singleton that other classes can get datas from
