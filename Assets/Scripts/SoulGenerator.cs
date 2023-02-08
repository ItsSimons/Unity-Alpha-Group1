using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulGenerator : MonoBehaviour
{
    public enum beliefOne {NAAA, AAAA};
    // NAAA does not show in afterlife
    // AAAA does believe in afterlife

    public enum beliefTwo {HAHA, HOHO, OCRA, OPRA};
    // HAHA serve in hell, then heaven
    // HOHO Only serve in one realm depending on balance
    // OCRA only in heaven
    // OPRA only in hell

    public enum beliefThree { ALF, RALF};
    // ALF do not reincarnate
    // RALF do reincarnate

    public beliefOne bl1;
    public beliefTwo bl2;
    public beliefThree bl3;

    void Start()
    {
        generateBeliefs();
    }

    public void generateBeliefs()
    {
        bl1 = (beliefOne)Random.Range(0, 2);
        bl2 = (beliefTwo)Random.Range(0, 4);
        bl3 = (beliefThree)Random.Range(0, 2);
    }
}
