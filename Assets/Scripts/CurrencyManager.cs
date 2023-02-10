using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField]public GameData gameData;
    [SerializeField] public int currency = 0;
    [SerializeField] public int passiveCost = 0;
    [SerializeField] public int passiveIncome = 0;

    public float time;

    // Start is called before the first frame update
    void Start()
    {
        currency = gameData.currency;
        float time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= time + 5f)
        {
            currency -= passiveCost;
            currency += passiveIncome;
            time = Time.time;
            //currencyAmount += 10;
        }
    }
}
