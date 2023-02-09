using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private ZoneManager zoneManager;

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

    private Sprite gate;

    void Start()
    {
        structure_prefab_1 = Resources.Load<GameObject>("Prefabs/Structures/Structure_1x1");
        structure_prefab_2 = Resources.Load<GameObject>("Prefabs/Structures/Structure_2x2");
        structure_prefab_3 = Resources.Load<GameObject>("Prefabs/Structures/Structure_3x3");

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Create2x2Building(zoneManager.GetZoneType());
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Create1x1Building(zoneManager.GetZoneType());
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            CreateGate();
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
            return true;
        }
        return false;
    }

    /// <summary>
    /// Creates a gate
    /// </summary>
    private void CreateGate()
    {
        GameObject newStructure = Instantiate(structure_prefab_3, Vector3.zero, Quaternion.identity);
        newStructure.transform.SetParent(structure_parent.transform.Find("Gates"));
        newStructure.GetComponentInChildren<SpriteRenderer>().sprite = gate;
        newStructure.AddComponent<Gate>();
        newStructure.GetComponent<Gate>().initGate(gameData, 10);
        newStructure.SetActive(false);

        zoneManager.CreatePreviewQuadOfSize(3, newStructure);
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
                if ((int)child.transform.position.x == tilePos.x && (int)child.transform.position.z == tilePos.y)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public Transform GetStructureParent()
    {
        return structure_parent;
    }
}
