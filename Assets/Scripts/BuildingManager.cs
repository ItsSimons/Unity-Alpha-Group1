using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public enum Structures {Gate};
    private float timer;
    private bool isHeaven;

    [SerializeField] private GameData gameData;
    [SerializeField] private ZoneManager zoneManager;
    [SerializeField] private VibesGrid vibesManager;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private Transform structure_parent;
    private GameObject structure_prefab_1;
    private GameObject structure_prefab_2;
    private GameObject structure_prefab_3;

    private Sprite structure_Green_1;
    private Sprite structure_Yellow_1;
    private Sprite structure_Orange_1;
    private Sprite structure_Brown_1;
    private Sprite structure_Purple_1;
    private Sprite structure_Red_1;
    private Sprite structure_Blue_1;

    private Sprite structure_Green_2;
    private Sprite structure_Yellow_2;
    private Sprite structure_Orange_2;
    private Sprite structure_Brown_2;
    private Sprite structure_Purple_2;
    private Sprite structure_Red_2;
    private Sprite structure_Blue_2;

    private Sprite rock_1;
    private Sprite rock_2;
    private Sprite rock_3;

    private Sprite gate;
    [SerializeField] private int GateSoulsPerSec;

    //Offset applied to the vibes grid to line up with the planes
    private Vector3 vibes_offset = Vector3.zero;

    void Awake()
    {
        isHeaven = zoneManager.GetHeavenBool();

        if (isHeaven)
        {
            InitHeavenSprite();
            vibes_offset = new Vector3(-502, 0, 0);
        }
        else
        {
            InitHellSprite();
            vibes_offset = new Vector3(-397, 0, 105);
        }

        structure_prefab_1 = Resources.Load<GameObject>("Prefabs/Structures/Structure_1x1");
        structure_prefab_2 = Resources.Load<GameObject>("Prefabs/Structures/Structure_2x2");
        structure_prefab_3 = Resources.Load<GameObject>("Prefabs/Structures/Structure_3x3");

        rock_1 = Resources.Load<Sprite>("Afterlife/Rocks/Rock_1");
        rock_2 = Resources.Load<Sprite>("Afterlife/Rocks/Rock_2");
        rock_3 = Resources.Load<Sprite>("Afterlife/Rocks/Rock_3");
    }

    private void InitHeavenSprite()
    {
        structure_Green_1 = Resources.Load<Sprite>("Afterlife/Building/Green/Building_Green_Heaven_1x1");
        structure_Yellow_1 = Resources.Load<Sprite>("Afterlife/Building/Yellow/Building_Yellow_Heaven_1x1");
        structure_Orange_1 = Resources.Load<Sprite>("Afterlife/Building/Orange/Building_Orange_Heaven_1x1");
        structure_Brown_1 = Resources.Load<Sprite>("Afterlife/Building/Brown/Building_Brown_Heaven_1x1");
        structure_Purple_1 = Resources.Load<Sprite>("Afterlife/Building/Purple/Building_Purple_Heaven_1x1");
        structure_Red_1 = Resources.Load<Sprite>("Afterlife/Building/Red/Building_Red_Heaven_1x1");
        structure_Blue_1 = Resources.Load<Sprite>("Afterlife/Building/Blue/Building_Blue_Heaven_1x1");

        structure_Green_2 = Resources.Load<Sprite>("Afterlife/Building/Green/Building_Green_Heaven_2x2");
        structure_Yellow_2 = Resources.Load<Sprite>("Afterlife/Building/Yellow/Building_Yellow_Heaven_2x2");
        structure_Orange_2 = Resources.Load<Sprite>("Afterlife/Building/Orange/Building_Orange_Heaven_2x2");
        structure_Brown_2 = Resources.Load<Sprite>("Afterlife/Building/Brown/Building_Brown_Heaven_2x2");
        structure_Purple_2 = Resources.Load<Sprite>("Afterlife/Building/Purple/Building_Purple_Heaven_2x2");
        structure_Red_2 = Resources.Load<Sprite>("Afterlife/Building/Red/Building_Red_Heaven_2x2");
        structure_Blue_2 = Resources.Load<Sprite>("Afterlife/Building/Blue/Building_Blue_Heaven_2x2");

        gate = Resources.Load<Sprite>("Afterlife/Gates/Gate_T1_Heaven_3x3");
    }

    private void InitHellSprite()
    {
        structure_Green_1 = Resources.Load<Sprite>("Afterlife/Building/Green/Building_Green_Hell_1x1");
        structure_Yellow_1 = Resources.Load<Sprite>("Afterlife/Building/Yellow/Building_Yellow_Hell_1x1");
        structure_Orange_1 = Resources.Load<Sprite>("Afterlife/Building/Orange/Building_Orange_Hell_1x1");
        structure_Brown_1 = Resources.Load<Sprite>("Afterlife/Building/Brown/Building_Brown_Hell_1x1");
        structure_Purple_1 = Resources.Load<Sprite>("Afterlife/Building/Purple/Building_Purple_Hell_1x1");
        structure_Red_1 = Resources.Load<Sprite>("Afterlife/Building/Red/Building_Red_Hell_1x1");
        structure_Blue_1 = Resources.Load<Sprite>("Afterlife/Building/Blue/Building_Blue_Hell_1x1");

        structure_Green_2 = Resources.Load<Sprite>("Afterlife/Building/Green/Building_Green_Hell_2x2");
        structure_Yellow_2 = Resources.Load<Sprite>("Afterlife/Building/Yellow/Building_Yellow_Hell_2x2");
        structure_Orange_2 = Resources.Load<Sprite>("Afterlife/Building/Orange/Building_Orange_Hell_2x2");
        structure_Brown_2 = Resources.Load<Sprite>("Afterlife/Building/Brown/Building_Brown_Hell_2x2");
        structure_Purple_2 = Resources.Load<Sprite>("Afterlife/Building/Purple/Building_Purple_Hell_2x2");
        structure_Red_2 = Resources.Load<Sprite>("Afterlife/Building/Red/Building_Red_Hell_2x2");
        structure_Blue_2 = Resources.Load<Sprite>("Afterlife/Building/Blue/Building_Blue_Hell_2x2");

        gate = Resources.Load<Sprite>("Afterlife/Gates/Gate_T1_Hell_3x3");
    }

    void Update()
    {
        CallEverySecond();
    }

    /// <summary>
    /// Calls functions within every 1 second instead of it being tied to FPS
    /// </summary>
    /// <param name="time"></param>
    private void CallEverySecond()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;

            GateManager();
        }
    }

    /// <summary>
    /// Generates a soul of a random type
    /// </summary>
    private void GateManager()
    {
        ZoneManager.ZoneType zone = (ZoneManager.ZoneType)Random.Range(0, 7);
        int numberOfGates = structure_parent.Find("Gates").childCount;

        switch (zone)
        {
            case ZoneManager.ZoneType.Green:
                if (isHeaven)
                {
                    gameData.souls_heaven_Green += GateSoulsPerSec * numberOfGates;
                }
                else
                {
                    gameData.souls_hell_Green += GateSoulsPerSec * numberOfGates;
                }
                break;

            case ZoneManager.ZoneType.Yellow:
                if (isHeaven)
                {
                    gameData.souls_heaven_Yellow += GateSoulsPerSec * numberOfGates;
                }
                else
                {
                    gameData.souls_hell_Yellow += GateSoulsPerSec * numberOfGates;
                }
                break;

            case ZoneManager.ZoneType.Orange:
                if (isHeaven)
                {
                    gameData.souls_heaven_Orange += GateSoulsPerSec * numberOfGates;
                }
                else
                {
                    gameData.souls_hell_Orange += GateSoulsPerSec * numberOfGates;
                }
                break;

            case ZoneManager.ZoneType.Brown:
                if (isHeaven)
                {
                    gameData.souls_heaven_Brown += GateSoulsPerSec * numberOfGates;
                }
                else
                {
                    gameData.souls_hell_Brown += GateSoulsPerSec * numberOfGates;
                }
                break;

            case ZoneManager.ZoneType.Purple:
                if (isHeaven)
                {
                    gameData.souls_heaven_Purple += GateSoulsPerSec * numberOfGates;
                }
                else
                {
                    gameData.souls_hell_Purple += GateSoulsPerSec * numberOfGates;
                }
                break;

            case ZoneManager.ZoneType.Red:
                if (isHeaven)
                {
                    gameData.souls_heaven_Red += GateSoulsPerSec * numberOfGates;
                }
                else
                {
                    gameData.souls_hell_Red += GateSoulsPerSec * numberOfGates;
                }
                break;

            case ZoneManager.ZoneType.Blue:
                if (isHeaven)
                {
                    gameData.souls_heaven_Blue += GateSoulsPerSec * numberOfGates;
                }
                else
                {
                    gameData.souls_hell_Blue += GateSoulsPerSec * numberOfGates;
                }
                break;

            default:
                return;
        }
    }

    /// <summary>
    /// Creates a 2x2 building on an empty zone tile
    /// </summary>
    /// <param name="zone">The zone to be filled with building</param>
    public bool Create2x2Building(ZoneManager.ZoneType zone)
    {
        Transform parent;
        Sprite sprite;
        switch (zone)
        {
            case ZoneManager.ZoneType.Green:
                sprite = structure_Green_2;
                parent = structure_parent.transform.Find("Green");
                break;

            case ZoneManager.ZoneType.Yellow:
                sprite = structure_Yellow_2;
                parent = structure_parent.transform.Find("Yellow");
                break;

            case ZoneManager.ZoneType.Orange:
                sprite = structure_Orange_2;
                parent = structure_parent.transform.Find("Orange");
                break;

            case ZoneManager.ZoneType.Brown:
                sprite = structure_Brown_2;
                parent = structure_parent.transform.Find("Brown");
                break;

            case ZoneManager.ZoneType.Purple:
                sprite = structure_Purple_2;
                parent = structure_parent.transform.Find("Purple");
                break;

            case ZoneManager.ZoneType.Red:
                sprite = structure_Red_2;
                parent = structure_parent.transform.Find("Red");
                break;

            case ZoneManager.ZoneType.Blue:
                sprite = structure_Blue_2;
                parent = structure_parent.transform.Find("Blue");
                break;

            default:
                return false;
        }

        Vector3 buildingPos = zoneManager.FindTileOfSize(zone, 2);
        if (buildingPos != Vector3.zero)
        {
            GameObject newStructure = Instantiate(structure_prefab_2, buildingPos, Quaternion.identity);
            newStructure.transform.SetParent(parent);
            newStructure.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            vibesManager.karmaChange2x2((int)(buildingPos.x + vibes_offset.x), (int)(buildingPos.z + vibes_offset.z), -1);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Creates a 1x1 building on an empty zone tile
    /// </summary>
    /// <param name="zone">The zone to be filled with building</param>
    public bool Create1x1Building(ZoneManager.ZoneType zone)
    {
        Transform parent;
        Sprite sprite;
        switch (zone)
        {
            case ZoneManager.ZoneType.Green:
                sprite = structure_Green_1;
                parent = structure_parent.transform.Find("Green");
                break;

            case ZoneManager.ZoneType.Yellow:
                sprite = structure_Yellow_1;
                parent = structure_parent.transform.Find("Yellow");
                break;

            case ZoneManager.ZoneType.Orange:
                sprite = structure_Orange_1;
                parent = structure_parent.transform.Find("Orange");
                break;

            case ZoneManager.ZoneType.Brown:
                sprite = structure_Brown_1;
                parent = structure_parent.transform.Find("Brown");
                break;

            case ZoneManager.ZoneType.Purple:
                sprite = structure_Purple_1;
                parent = structure_parent.transform.Find("Purple");
                break;

            case ZoneManager.ZoneType.Red:
                sprite = structure_Red_1;
                parent = structure_parent.transform.Find("Red");
                break;

            case ZoneManager.ZoneType.Blue:
                sprite = structure_Blue_1;
                parent = structure_parent.transform.Find("Blue");
                break;

            default:
                return false;
        }

        Vector3 buildingPos = zoneManager.FindTileOfSize(zone, 1);
        if (buildingPos != Vector3.zero)
        {
            GameObject newStructure = Instantiate(structure_prefab_1, buildingPos, Quaternion.identity);
            newStructure.transform.SetParent(parent);
            newStructure.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            vibesManager.karmaChange1x1((int)(buildingPos.x + vibes_offset.x), (int)(buildingPos.z + vibes_offset.z), -1);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Creates a gate
    /// </summary>
    public void CreateGateButton()
    {
        zoneManager.CreatePreviewQuadOfSize(3, Structures.Gate);
    }

    /// <summary>
    /// Instantiates a gate at position
    /// </summary>
    /// <param name="pos">Position of gate</param>
    public void InstatiateGate(Vector3 pos)
    {
        GameObject newStructure = Instantiate(structure_prefab_3, pos, Quaternion.identity);
        newStructure.transform.SetParent(structure_parent.transform.Find("Gates"));
        newStructure.GetComponentInChildren<SpriteRenderer>().sprite = gate;
        //Gate vibes
        vibesManager.karmaChange3x3((int)(pos.x + vibes_offset.x), (int)(pos.z + vibes_offset.z), -1);
        if (isHeaven)
        {
            audioManager.generateSound(AudioManager.SoundName.GateHeaven);
        }
        else
        {
            audioManager.generateSound(AudioManager.SoundName.GateHell);
        }
    }

    /// <summary>
    /// Removes a structure on a given tile
    /// </summary>
    /// <param name="tilePos">Position of the tile</param>
    public void RemoveBuilding(Vector3Int tilePos)
    {
        foreach (Transform zone_parent in structure_parent.transform)
        {
            foreach (Transform child in zone_parent)
            {
                if ((int)child.transform.localPosition.x == tilePos.x && (int)child.transform.localPosition.z == tilePos.y)
                {
                    Destroy(child.gameObject);
                    audioManager.generateSound(AudioManager.SoundName.Demolish1);
                }
            }
        }
    }

    public void CreateRandomRock(Vector3 pos)
    {
        int random = Random.Range(0, 3);
        Sprite rockSprite;

        switch (random)
        {
            case 0:
                rockSprite = rock_1;
                break;

            case 1:
                rockSprite = rock_2;
                break;

            default:
                rockSprite = rock_3;
                break;
        }

        GameObject newStructure = Instantiate(structure_prefab_1, pos, Quaternion.identity);
        newStructure.transform.SetParent(structure_parent.transform.Find("Rocks"));
        newStructure.GetComponentInChildren<SpriteRenderer>().sprite = rockSprite;
    }

    public Transform GetStructureParent()
    {
        return structure_parent;
    }
}
