using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public enum Structures {Gate, Topias, TrainingCenters};
    private float timer;
    private bool isHeaven;

    [SerializeField] private GameData gameData;
    [SerializeField] private ZoneManager zoneManager;
    [SerializeField] private PopulationManager populationManager;
    [SerializeField] private VibesGrid vibesManager;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private Transform structure_parent;
    private GameObject structure_prefab_1;
    private GameObject structure_prefab_2;
    private GameObject structure_prefab_3;
    private GameObject road_prefab;

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
    private Sprite topias;
    private Sprite trainingCenter;
    private Sprite karmaAnchor;

    private Sprite road_Corner_1;
    private Sprite road_Corner_2;
    private Sprite road_Corner_3;
    private Sprite road_Corner_4;

    private Sprite road_Intersection_1;
    private Sprite road_Intersection_2;
    private Sprite road_Intersection_3;
    private Sprite road_Intersection_4;
    private Sprite road_Intersection_5;

    private Sprite road_Straight_1;
    private Sprite road_Straight_2;

    private List<Vector3Int> newRoadPos;

    [SerializeField] private int GateSoulsPerSec;
    [SerializeField] private int TrainingCenterRate;
    [SerializeField] private float currencyPerSoul;

    //Offset applied to the vibes grid to line up with the planes
    private Vector3 vibes_offset = Vector3.zero;

    void Awake()
    {
        newRoadPos = new List<Vector3Int>();

        isHeaven = zoneManager.IsThisHeaven();

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
        road_prefab = Resources.Load<GameObject>("Prefabs/Structures/Road_1x1");

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
        karmaAnchor = Resources.Load<Sprite>("Afterlife/Karma/KA_Heaven_3x3");
        topias = Resources.Load<Sprite>("Afterlife/Topias/Topias_T1_Heaven_4x4");
        trainingCenter = Resources.Load<Sprite>("Afterlife/Training Centers/TC_T1_Heaven_3x3");

        road_Corner_1 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Corner_1_Heaven_1x1");
        road_Corner_2 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Corner_2_Heaven_1x1");
        road_Corner_3 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Corner_3_Heaven_1x1");
        road_Corner_4 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Corner_4_Heaven_1x1");

        road_Intersection_1 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Intersection_1_Heaven_1x1");
        road_Intersection_2 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Intersection_2_Heaven_1x1");
        road_Intersection_3 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Intersection_3_Heaven_1x1");
        road_Intersection_4 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Intersection_4_Heaven_1x1");
        road_Intersection_5 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Intersection_5_Heaven_1x1");

        road_Straight_1 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Straight_1_Heaven_1x1");
        road_Straight_2 = Resources.Load<Sprite>("Afterlife/Roads/Heaven/Road_Straight_2_Heaven_1x1");
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
        karmaAnchor = Resources.Load<Sprite>("Afterlife/Karma/KA_Hell_3x3");
        topias = Resources.Load<Sprite>("Afterlife/Topias/Topias_T1_Hell_4x4");
        trainingCenter = Resources.Load<Sprite>("Afterlife/Training Centers/TC_T1_Hell_3x3");

        road_Corner_1 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Corner_1_Hell_1x1");
        road_Corner_2 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Corner_2_Hell_1x1");
        road_Corner_3 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Corner_3_Hell_1x1");
        road_Corner_4 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Corner_4_Hell_1x1");

        road_Intersection_1 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Intersection_1_Hell_1x1");
        road_Intersection_2 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Intersection_2_Hell_1x1");
        road_Intersection_3 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Intersection_3_Hell_1x1");
        road_Intersection_4 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Intersection_4_Hell_1x1");
        road_Intersection_5 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Intersection_5_Hell_1x1");

        road_Straight_1 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Straight_1_Hell_1x1");
        road_Straight_2 = Resources.Load<Sprite>("Afterlife/Roads/Hell/Road_Straight_2_Hell_1x1");
    }

    void Update()
    {
        CallEverySecond();

        if (Input.GetKeyDown(KeyCode.B))
        {
            CreateTCButton();
        }
        if (Input.GetKey(KeyCode.T))
        {
            CreateTopiaButton();
        }
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
            TrainingCenterManager();
        }
    }

    /// <summary>
    /// Generates a soul of a random type
    /// </summary>
    private void GateManager()
    {
        ZoneManager.ZoneType zone = (ZoneManager.ZoneType)Random.Range(0, 7);
        int numberOfGates = structure_parent.Find("Gates").childCount;
        gameData.currency += currencyPerSoul * GateSoulsPerSec * numberOfGates;

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
    /// Converts a random soul type to a demon/angel
    /// </summary>
    private void TrainingCenterManager()
    {
        if (populationManager.IsTopiasFull())
        {
            return;
        }

        ZoneManager.ZoneType zone = (ZoneManager.ZoneType)Random.Range(0, 7);
        int numberOfAngelsDemons = structure_parent.Find("TrainingCenters").childCount * TrainingCenterRate; ;
        if (isHeaven)
        {
            gameData.angels += numberOfAngelsDemons;
            switch (zone)
            {
                case ZoneManager.ZoneType.Green:
                    gameData.souls_heaven_Green -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Yellow:
                    gameData.souls_heaven_Yellow -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Orange:
                    gameData.souls_heaven_Orange -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Brown:
                    gameData.souls_heaven_Brown -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Purple:
                    gameData.souls_heaven_Purple -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Red:
                    gameData.souls_heaven_Red -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Blue:
                    gameData.souls_heaven_Blue -= numberOfAngelsDemons;
                    break;
            }
        }
        else
        {
            gameData.demons += structure_parent.Find("TrainingCenters").childCount * TrainingCenterRate;
            switch (zone)
            {
                case ZoneManager.ZoneType.Green:
                    gameData.souls_hell_Green -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Yellow:
                    gameData.souls_hell_Yellow -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Orange:
                    gameData.souls_hell_Orange -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Brown:
                    gameData.souls_hell_Brown -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Purple:
                    gameData.souls_hell_Purple -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Red:
                    gameData.souls_hell_Red -= numberOfAngelsDemons;
                    break;

                case ZoneManager.ZoneType.Blue:
                    gameData.souls_hell_Blue -= numberOfAngelsDemons;
                    break;
            }
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
    /// Creates a Training Center
    /// </summary>
    public void CreateTCButton()
    {
        zoneManager.CreatePreviewQuadOfSize(3, Structures.TrainingCenters);
    }

    /// <summary>
    /// Instantiates a training center at position
    /// </summary>
    /// <param name="pos">Position of training center</param>
    public void InstantiateTrainingCenter(Vector3 pos)
    {
        GameObject newStructure = Instantiate(structure_prefab_3, pos, Quaternion.identity);
        newStructure.transform.SetParent(structure_parent.transform.Find("TrainingCenters"));
        newStructure.GetComponentInChildren<SpriteRenderer>().sprite = trainingCenter;
        if (isHeaven)
        {
            audioManager.generateSound(AudioManager.SoundName.InstituteHeaven);
        }
        else
        {
            audioManager.generateSound(AudioManager.SoundName.InstituteHell);
        }
    }

    /// <summary>
    /// Creates a Topias
    /// </summary>
    public void CreateTopiaButton()
    {
        zoneManager.CreatePreviewQuadOfSize(4, Structures.Topias);
    }

    /// <summary>
    /// Instantiates a topia at position
    /// </summary>
    /// <param name="pos">Position of topia</param>
    public void InstantiateTopia(Vector3 pos)
    {
        GameObject newStructure = Instantiate(structure_prefab_3, pos, Quaternion.identity);
        newStructure.transform.SetParent(structure_parent.transform.Find("Topias"));
        newStructure.GetComponentInChildren<SpriteRenderer>().sprite = topias;
        if (isHeaven)
        {
            newStructure.transform.GetChild(0).GetComponentInChildren<Transform>().localPosition = new Vector3(1.05f, 3.25f, 1.05f);
            audioManager.generateSound(AudioManager.SoundName.TopiaHeaven);
        }
        else
        {
            newStructure.transform.GetChild(0).GetComponentInChildren<Transform>().localPosition = new Vector3(1.05f, 4.25f, 1.05f);
            audioManager.generateSound(AudioManager.SoundName.TopiaHell);
        }
    }

    /// <summary>
    /// Creates a road at position
    /// </summary>
    /// <param name="pos">Position of road</param>
    public void InstantiateRoad(Vector3 pos)
    {
        GameObject newStructure = Instantiate(road_prefab, pos, Quaternion.identity);
        newStructure.transform.SetParent(structure_parent.transform.Find("Roads"));
        newStructure.GetComponentInChildren<SpriteRenderer>().sprite = road_Corner_1;
    }

    /// <summary>
    /// Adds a road tilePos to the newRoadPos list
    /// </summary>
    /// <param name="tilePos">tilePos of road</param>
    public void AddNewRoad(Vector3Int tilePos)
    {
        newRoadPos.Add(tilePos);
    }

    /// <summary>
    /// Clears the newRoadPos list
    /// </summary>
    public void ClearNewRoadList()
    {
        newRoadPos.Clear();
    }

    /// <summary>
    /// Adjusts the sprite of roads near the roads contained in newRoadPos list
    /// </summary>
    public void AdjustNearbyRoadSprite()
    {
        foreach (Vector3Int pos in newRoadPos)
        {
            Vector3Int rightPos = pos + new Vector3Int(1, 0);
            Vector3Int leftPos = pos + new Vector3Int(-1, 0);
            Vector3Int upPos = pos + new Vector3Int(0, 1);
            Vector3Int downPos = pos + new Vector3Int(0, -1);

            AdjustThisRoadSprite(pos);
            AdjustThisRoadSprite(rightPos);
            AdjustThisRoadSprite(leftPos);
            AdjustThisRoadSprite(upPos);
            AdjustThisRoadSprite(downPos);
        }
    }

    /// <summary>
    /// Adjust the sprite of the road at tilePos
    /// </summary>
    /// <param name="tilePos">tilePos of road to be adjusted</param>
    private void AdjustThisRoadSprite(Vector3Int tilePos)
    {
        Vector3Int rightPos = tilePos + new Vector3Int(1, 0);
        Vector3Int leftPos = tilePos + new Vector3Int(-1, 0);
        Vector3Int upPos = tilePos + new Vector3Int(0, 1);
        Vector3Int downPos = tilePos + new Vector3Int(0, -1);

        bool right = false;
        bool left = false;
        bool up = false;
        bool down = false;

        Transform road = null;

        foreach (Transform child in structure_parent.transform.Find("Roads"))
        {
            if ((int)child.transform.localPosition.x == tilePos.x && (int)child.transform.localPosition.z == tilePos.y)
            {
                road = child;
            }

            if ((int)child.transform.localPosition.x == rightPos.x && (int)child.transform.localPosition.z == rightPos.y)
            {
                right = true;
            }

            if ((int)child.transform.localPosition.x == leftPos.x && (int)child.transform.localPosition.z == leftPos.y)
            {
                left = true;
            }

            if ((int)child.transform.localPosition.x == upPos.x && (int)child.transform.localPosition.z == upPos.y)
            {
                up = true;
            }

            if ((int)child.transform.localPosition.x == downPos.x && (int)child.transform.localPosition.z == downPos.y)
            {
                down = true;
            }
        }

        if (road == null)
        {
            return;
        }

        if (left && !right && !up && down)
        {
            road.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Corner_1;
            if (isHeaven)
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.5f, 0.45f, 0.5f);
            }
            else
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, 0.32f, 0.65f);
            }
        }
        else if (left && !right && up && !down)
        {
            road.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Corner_2;
            if (isHeaven)
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.55f, -0.2f, 0.65f);
            }
            else
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.53f, 0.17f, 0.62f);
            }
        }
        else if (!left && right && up && !down)
        {
            road.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Corner_3;
            if (isHeaven)
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.8f, 0.2f, 0.8f);
            }
            else
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, 0.18f, 0.65f);
            }
        }
        else if (!left && right && !up && down)
        {
            road.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Corner_4;
            if (isHeaven)
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.87f, -0.46f, 0.92f);
            }
            else
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, 0.1f, 0.65f);
            }
        }
        else if (left && right && up && down)
        {
            road.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Intersection_1;
            if (isHeaven)
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.6f, -0.18f, 0.6f);
            }
        }
        else if (left && right && !up && down)
        {
            road.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Intersection_2;
            if (isHeaven)
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, -0.25f, 0.65f);
            }
            else
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, 0.29f, 0.65f);
            }
        }
        else if (left && !right && up && down)
        {
            road.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Intersection_3;
            if (isHeaven)
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, -0.25f, 0.65f);
            }
            else
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, 0.32f, 0.65f);
            }
        }
        else if (left && right && up &&!down)
        {
            road.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Intersection_4;
            if (isHeaven)
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.74f, 0.14f, 0.65f);
            }
            else
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, 0.17f, 0.65f);
            }
        }
        else if (!left && right && up && down)
        {
            road.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Intersection_5;
            if (isHeaven)
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, 0.15f, 0.77f);
            }
            else
            {
                road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, 0.2f, 0.65f);
            }
        }
        else if (left || right)
        {
            road.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Straight_1;
            road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, 0.15f, 0.65f);
        }
        else if (up || down)
        {
            road.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = road_Straight_2;
            road.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3(0.65f, 0.15f, 0.65f);
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
                    int rand_seed = Random.Range(0, 3);
                    switch (rand_seed)
                    {
                        case 0:
                            audioManager.generateSound(AudioManager.SoundName.Demolish1);
                            break;
                        case 1:
                            audioManager.generateSound(AudioManager.SoundName.Demolish2);
                            break;
                        case 2:
                            audioManager.generateSound(AudioManager.SoundName.Demolish3);
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Creates a rock of a random sprite at given position
    /// </summary>
    /// <param name="pos">Position of rock</param>
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

    /// <summary>
    /// Create a Karma Portal Anchor at the given position
    /// </summary>
    /// <param name="pos">Position of Karma Portal Anchor</param>
    public void CreateKarmaAnchor(Vector3 pos)
    {
        GameObject newStructure = Instantiate(structure_prefab_3, pos, Quaternion.identity);
        newStructure.transform.SetParent(structure_parent.transform.Find("KarmaAnchors"));
        newStructure.GetComponentInChildren<SpriteRenderer>().sprite = karmaAnchor;

        if (isHeaven)
        {
            newStructure.transform.GetChild(0).GetComponentInChildren<Transform>().localPosition = new Vector3(1.05f, 4.5f, 1.05f);
        }
        else
        {
            newStructure.transform.GetChild(0).GetComponentInChildren<Transform>().localPosition = new Vector3(1.05f, 3.6f, 1.05f);
        }
    }

    public Transform GetStructureParent()
    {
        return structure_parent;
    }
}
