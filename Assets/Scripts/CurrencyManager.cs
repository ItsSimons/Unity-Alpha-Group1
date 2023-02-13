using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] public float passiveCost;
    [SerializeField] public float passiveIncome;
    
    [SerializeField] private GameData gameData;
    [SerializeField] private float tileCost;
    [SerializeField] private float gateCost;
    [SerializeField] private float trainingCenterCost;
    [SerializeField] private float soulsDivider;

    private float timer;

    void Start()
    {
        gameData.currency = 10000;
        float time = Time.time;
    }

    void Update()
    {
        CallEverySeconds(5);
        UpdateTotalSouls();
    }

    /// <summary>
    /// Calls functions within at an interval set by the time value
    /// </summary>
    /// <param name="time">Time between each call</param>
    private void CallEverySeconds(float time)
    {
        timer += Time.deltaTime;
        if (timer > time)
        {
            timer = 0;

            // Put functions here
            UpdateCurrency();
        }
    }

    private void UpdateCurrency()
    {
        passiveIncome += gameData.souls_total / soulsDivider;
        gameData.currency -= passiveCost;
        gameData.currency += passiveIncome;
    }

    private void UpdateTotalSouls()
    {
        gameData.souls_total = (gameData.souls_heaven_Green + gameData.souls_hell_Green + gameData.souls_heaven_Yellow + gameData.souls_hell_Yellow +
        gameData.souls_heaven_Orange + gameData.souls_hell_Orange + gameData.souls_heaven_Brown + gameData.souls_hell_Brown + gameData.souls_heaven_Purple
        + gameData.souls_hell_Purple + gameData.souls_heaven_Red + gameData.souls_hell_Red + gameData.souls_heaven_Blue + gameData.souls_hell_Blue);
    }

    /// <summary>
    /// Handles the transactions of tiles
    /// </summary>
    /// <param name="numberOfTiles">Number of tiles bought</param>
    public void TransactionTile(int numberOfTiles)
    {
        gameData.currency -= tileCost * numberOfTiles;
    }

    /// <summary>
    /// Handles the transactions of structures
    /// </summary>
    /// <param name="structure">The structure bought</param>
    public void TransactionStructures(BuildingManager.Structures structure)
    {
        switch (structure)
        {
            case BuildingManager.Structures.Gate:
                gameData.currency -= gateCost;
                break;

            case BuildingManager.Structures.TrainingCenters:
                gameData.currency -= trainingCenterCost;
                break;
        }
    }
}
