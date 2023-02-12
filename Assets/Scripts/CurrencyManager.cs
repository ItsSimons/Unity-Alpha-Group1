using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private float passiveCost;
    [SerializeField] private float tileCost;
    [SerializeField] private float gateCost;

    [SerializeField] private float passiveIncome;

    private float timer;

    void Start()
    {
        gameData.currency = 10000;
        float time = Time.time;
    }

    void Update()
    {
        CallEverySeconds(5);
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
        passiveIncome += gameData.souls_total / 10;
        gameData.currency -= passiveCost;
        gameData.currency += passiveIncome;
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
        }
    }
}
