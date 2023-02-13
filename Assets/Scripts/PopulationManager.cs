using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private ZoneManager zoneManager;
    [SerializeField] private int capacityPerBuilding;
    [SerializeField] private int capacityPerTopia;

    public int capacity_Green;
    public int capacity_Yellow;
    public int capacity_Orange;
    public int capacity_Brown;
    public int capacity_Purple;
    public int capacity_Red;
    public int capacity_Blue;

    public int capacity_Angels;
    public int capacity_Demons;

    private bool isFull_Green;
    private bool isFull_Yellow;
    private bool isFull_Orange;
    private bool isFull_Brown;
    private bool isFull_Purple;
    private bool isFull_Red;
    private bool isFull_Blue;

    private bool isHeaven;

    private void Awake()
    {
        isHeaven = zoneManager.IsThisHeaven();
    }

    private void Start()
    {
        isFull_Green = false;
        isFull_Yellow = false;
        isFull_Orange = false;
        isFull_Brown = false;
        isFull_Purple = false;
        isFull_Red = false;
        isFull_Blue = false;
    }

    void FixedUpdate()
    {
        CheckCapacity();
        
        if (isHeaven)
        {
            CheckPopulationHeaven();
        }
        else
        {
            CheckPopulationHell();
        }
    }

    /// <summary>
    /// Check the capacity of each zone
    /// </summary>
    private void CheckCapacity()
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
    /// Create a new building in Heaven if population exceeds capacity
    /// </summary>
    private void CheckPopulationHeaven()
    {
        if (gameData.souls_heaven_Green > capacity_Green && !isFull_Green)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Green))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Green))
                {
                    isFull_Green = true;
                }
            }
        }

        if (gameData.souls_heaven_Yellow > capacity_Yellow && !isFull_Yellow)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Yellow))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Yellow))
                {
                    isFull_Yellow = true;
                }
            }
        }

        if (gameData.souls_heaven_Orange > capacity_Orange && !isFull_Orange)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Orange))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Orange))
                {
                    isFull_Orange = true;
                }
            }
        }

        if (gameData.souls_heaven_Brown > capacity_Brown && !isFull_Brown)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Brown))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Brown))
                {
                    isFull_Brown = true;
                }
            }
        }

        if (gameData.souls_heaven_Purple > capacity_Purple && !isFull_Purple)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Purple))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Purple))
                {
                    isFull_Purple = true;
                }
            }
        }

        if (gameData.souls_heaven_Red > capacity_Red && !isFull_Red)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Red))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Red))
                {
                    isFull_Red = true;
                }
            }
        }

        if (gameData.souls_heaven_Blue > capacity_Blue && !isFull_Blue)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Blue))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Blue))
                {
                    isFull_Blue = true;
                }
            }
        }
    }

    /// <summary>
    /// Create a new building in Hell if population exceeds capacity
    /// </summary>
    private void CheckPopulationHell()
    {
        if (gameData.souls_hell_Green > capacity_Green && !isFull_Green)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Green))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Green))
                {
                    isFull_Green = true;
                }
            }
        }

        if (gameData.souls_hell_Yellow > capacity_Yellow && !isFull_Yellow)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Yellow))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Yellow))
                {
                    isFull_Yellow = true;
                }
            }
        }

        if (gameData.souls_hell_Orange > capacity_Orange && !isFull_Orange)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Orange))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Orange))
                {
                    isFull_Orange = true;
                }
            }
        }

        if (gameData.souls_hell_Brown > capacity_Brown && !isFull_Brown)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Brown))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Brown))
                {
                    isFull_Brown = true;
                }
            }
        }

        if (gameData.souls_hell_Purple > capacity_Purple && !isFull_Purple)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Purple))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Purple))
                {
                    isFull_Purple = true;
                }
            }
        }

        if (gameData.souls_hell_Red > capacity_Red && !isFull_Red)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Red))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Red))
                {
                    isFull_Red = true;
                }
            }
        }

        if (gameData.souls_hell_Blue > capacity_Blue && !isFull_Blue)
        {
            if (!buildingManager.Create2x2Building(ZoneManager.ZoneType.Blue))
            {
                if (!buildingManager.Create1x1Building(ZoneManager.ZoneType.Blue))
                {
                    isFull_Blue = true;
                }
            }
        }
    }

    /// <summary>
    /// Set zone as not full so more buildings can be made
    /// </summary>
    /// <param name="zone">The zone to be set as not full</param>
    public void SetZoneNotFull(ZoneManager.ZoneType zone)
    {
        switch (zone)
        {
            case ZoneManager.ZoneType.Green:
                isFull_Green = false;
                break;

            case ZoneManager.ZoneType.Yellow:
                isFull_Yellow = false;
                break;

            case ZoneManager.ZoneType.Orange:
                isFull_Orange = false;
                break;

            case ZoneManager.ZoneType.Brown:
                isFull_Brown = false;
                break;

            case ZoneManager.ZoneType.Purple:
                isFull_Purple = false;
                break;

            case ZoneManager.ZoneType.Red:
                isFull_Red = false;
                break;

            case ZoneManager.ZoneType.Blue:
                isFull_Blue = false;
                break;
        }
    }

    /// <summary>
    /// Checks if topias for the plane is full
    /// </summary>
    /// <returns>True if full, False if not full</returns>
    public bool IsTopiasFull()
    {
        if (isHeaven)
        {
            capacity_Angels = buildingManager.GetStructureParent().Find("Topias").childCount * capacityPerTopia;
            if (gameData.angels >= capacity_Angels)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            capacity_Demons = buildingManager.GetStructureParent().Find("Topias").childCount * capacityPerTopia;
            if (gameData.demons >= capacity_Demons)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
