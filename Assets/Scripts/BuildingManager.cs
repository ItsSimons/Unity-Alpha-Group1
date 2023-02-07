using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private ZoneManager zoneManager;

    [SerializeField] private Transform structure_parent;
    [SerializeField] private GameObject structure_prefab;

    private Sprite structure_Green;
    private Sprite structure_Yellow;
    private Sprite structure_Orange;
    private Sprite structure_Brown;
    private Sprite structure_Purple;
    private Sprite structure_Red;
    private Sprite structure_Blue;

    void Start()
    {
        structure_Green = Resources.Load<Sprite>("Afterlife/Building/Green/Building_Green_Heaven_1x1");
        structure_Yellow = Resources.Load<Sprite>("Afterlife/Building/Yellow/Building_Yellow_Heaven_1x1");
        structure_Orange = Resources.Load<Sprite>("Afterlife/Building/Orange/Building_Orange_Heaven_1x1");
        structure_Brown = Resources.Load<Sprite>("Afterlife/Building/Brown/Building_Brown_Heaven_1x1");
        structure_Purple = Resources.Load<Sprite>("Afterlife/Building/Purple/Building_Purple_Heaven_1x1");
        structure_Red = Resources.Load<Sprite>("Afterlife/Building/Red/Building_Red_Heaven_1x1");
        structure_Blue = Resources.Load<Sprite>("Afterlife/Building/Blue/Building_Blue_Heaven_1x1");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            CreateBuilding(zoneManager.GetZoneType());
        }
    }

    /// <summary>
    /// Creates a structure on an empty zone tile
    /// </summary>
    /// <param name="zone">The zone to be filled with structure</param>
    public void CreateBuilding(ZoneManager.ZoneType zone)
    {
        Transform parent;
        Sprite sprite;
        switch (zone)
        {
            case ZoneManager.ZoneType.Green:
                sprite = structure_Green;
                parent = structure_parent.transform.Find("Green");
                break;

            case ZoneManager.ZoneType.Yellow:
                sprite = structure_Yellow;
                parent = structure_parent.transform.Find("Yellow");
                break;

            case ZoneManager.ZoneType.Orange:
                sprite = structure_Orange;
                parent = structure_parent.transform.Find("Orange");
                break;

            case ZoneManager.ZoneType.Brown:
                sprite = structure_Brown;
                parent = structure_parent.transform.Find("Brown");
                break;

            case ZoneManager.ZoneType.Purple:
                sprite = structure_Purple;
                parent = structure_parent.transform.Find("Purple");
                break;

            case ZoneManager.ZoneType.Red:
                sprite = structure_Red;
                parent = structure_parent.transform.Find("Red");
                break;

            case ZoneManager.ZoneType.Blue:
                sprite = structure_Blue;
                parent = structure_parent.transform.Find("Blue");
                break;

            default:
                return;
        }

        Vector3 buildingPos = zoneManager.GetBuildingPos(zone);
        if (buildingPos != Vector3.zero)
        {
            GameObject newStructure = Instantiate(structure_prefab, buildingPos, Quaternion.identity);
            newStructure.transform.SetParent(parent);
            newStructure.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        }
    }

    /// <summary>
    /// Removes a structure on a given tile
    /// </summary>
    /// <param name="tilePos">Position of the tile</param>
    public void RemoveBuilding(Vector3Int tilePos)
    {
        for (var i = 0; i < structure_parent.childCount; i++)
        {
            if ((int)structure_parent.GetChild(i).transform.position.x == tilePos.x && (int)structure_parent.GetChild(i).transform.position.z == tilePos.y)
            {
                Destroy(structure_parent.GetChild(i).gameObject);
            }
        }
    }
}
