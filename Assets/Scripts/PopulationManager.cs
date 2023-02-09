using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private int capacityPerBuilding;

    public int capacity_Green;
    public int capacity_Yellow;
    public int capacity_Orange;
    public int capacity_Brown;
    public int capacity_Purple;
    public int capacity_Red;
    public int capacity_Blue;

    void FixedUpdate()
    {
        checkCapacity();
        checkPopulation();
    }

    /// <summary>
    /// Check the capacity of each zone
    /// </summary>
    private void checkCapacity()
    {
        capacity_Green = buildingManager.GetStructureParent().Find("Green").childCount * capacityPerBuilding;
        capacity_Yellow = buildingManager.GetStructureParent().Find("Yellow").childCount * capacityPerBuilding;
        capacity_Orange = buildingManager.GetStructureParent().Find("Orange").childCount * capacityPerBuilding;
        capacity_Brown = buildingManager.GetStructureParent().Find("Brown").childCount * capacityPerBuilding;
        capacity_Purple = buildingManager.GetStructureParent().Find("Purple").childCount * capacityPerBuilding;
        capacity_Red = buildingManager.GetStructureParent().Find("Red").childCount * capacityPerBuilding;
        capacity_Blue = buildingManager.GetStructureParent().Find("Blue").childCount * capacityPerBuilding;
    }

    /// <summary>
    /// Create a new building if population exceeds capacity
    /// </summary>
    private void checkPopulation()
    {
        if (gameData.souls_heaven_Green > capacity_Green)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Green))
            {
                buildingManager.Create1x1Building(ZoneManager.ZoneType.Green);
            }
        }

        if (gameData.souls_heaven_Yellow > capacity_Yellow)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Yellow))
            {
                buildingManager.Create1x1Building(ZoneManager.ZoneType.Yellow);
            }
        }

        if (gameData.souls_heaven_Orange > capacity_Orange)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Orange))
            {
                buildingManager.Create1x1Building(ZoneManager.ZoneType.Orange);
            }
        }

        if (gameData.souls_heaven_Brown > capacity_Brown)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Brown))
            {
                buildingManager.Create1x1Building(ZoneManager.ZoneType.Brown);
            }
        }

        if (gameData.souls_heaven_Purple > capacity_Purple)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Purple))
            {
                buildingManager.Create1x1Building(ZoneManager.ZoneType.Purple);
            }
        }

        if (gameData.souls_heaven_Red > capacity_Red)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Red))
            {
                buildingManager.Create1x1Building(ZoneManager.ZoneType.Red);
            }
        }

        if (gameData.souls_heaven_Blue > capacity_Blue)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Blue))
            {
                buildingManager.Create1x1Building(ZoneManager.ZoneType.Blue);
            }
        }
    }
}
