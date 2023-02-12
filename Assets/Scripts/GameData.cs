using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public float currency = 0;

    // total souls in heaven

    public int souls_heaven_Green;
    public int souls_heaven_Yellow;
    public int souls_heaven_Orange;
    public int souls_heaven_Brown;
    public int souls_heaven_Purple;
    public int souls_heaven_Red;
    public int souls_heaven_Blue;

    // total souls in hell

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

    // number of souls of certain religious beliefs

    public int HAHAALFSUMA;
    public int HAHAALFSUSA;
    public int HAHARALFSUMA;
    public int HAHARALFSUSA;
    public int HOHOALFSUMA;
    public int HOHOALFSUSA;
    public int HOHORALFSUMA;
    public int HOHORALFSUSA;
    public int OCRAALFSUMA;
    public int OCRAALFSUSA;
    public int OCRARALFSUMA;
    public int OCRARALFSUSA;
    public int OPRAALFSUMA;
    public int OPRAALFSUSA;
    public int OPRARALFSUMA;
    public int OPRARALFSUSA;

    // ----1----
    // HAHA serve in hell, then heaven
    // HOHO Only serve in one realm depending on balance
    // OCRA only in heaven
    // OPRA only in hell
    // ----2----
    // SUMA serve all sins and virtues (multiple)
    // SUSA serve one sin or virtue (single)
    // ----3----
    // ALF do not reincarnate
    // RALF do reincarnate

}

// Don't put functions here, GameData is a singleton that other classes can get datas from
