using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZoneManager : MonoBehaviour
{
    public enum ZoneType {Green, Yellow, Orange, Brown, Purple, Red, Blue, Erase}
    private ZoneType zoneType;

    [SerializeField] BuildingManager buildingManager;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private GameObject previewQuad;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private int tilemapSize;

    [SerializeField] private TileBase tile_Green;
    [SerializeField] private TileBase tile_Yellow;
    [SerializeField] private TileBase tile_Orange;
    [SerializeField] private TileBase tile_Brown;
    [SerializeField] private TileBase tile_Purple;
    [SerializeField] private TileBase tile_Red;
    [SerializeField] private TileBase tile_Blue;

    private TileBase selectedTile;

    private Vector3 mouse_down;
    private Vector3 mouse_up;
    private Vector3 mouse_drag;

    private List<Vector3Int> occupiedTiles;

    private bool isBuilding;
    private bool ignoreFirstInput;

    private void Awake()
    {
        grid = gridLayout.gameObject.GetComponent<Grid>();
        previewQuad.SetActive(false);
        occupiedTiles = new List<Vector3Int>();
        isBuilding = false;
    }

    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        MouseInputs();

        KeyboardInputs();
    }

    private void MouseInputs()
    {
        if (isBuilding)
        {
            StartPreviewQuad(WorldPosToGridPos(GetMouseWorldPos()), zoneType);
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouse_down = WorldPosToGridPos(GetMouseWorldPos());
        }
        else if (Input.GetMouseButton(0))
        {
            mouse_drag = WorldPosToGridPos(GetMouseWorldPos());

            if (isBuilding)
            {
                ResizePreviewQuad(mouse_down, mouse_drag);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouse_up = WorldPosToGridPos(GetMouseWorldPos());

            if (isBuilding)
            {
                BoxFill(tilemap, selectedTile, mouse_down, mouse_up);

                previewQuad.transform.localScale = Vector3.one;
                previewQuad.SetActive(false);
                isBuilding = false;
            }

            if (ignoreFirstInput)
            {
                ignoreFirstInput = false;
                isBuilding = true;
            }
        }
    }

    private void KeyboardInputs()
    {
        // 1 = Green
        // 2 = Yellow
        // 3 = Orange
        // 4 = Brown
        // 5 = Purple
        // 6 = Red
        // 7 = Blue
        // 8 = Erase
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ignoreFirstInput = false;
            isBuilding = true;
            StartZoneBuilding(ZoneType.Green);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ignoreFirstInput = false;
            isBuilding = true;
            StartZoneBuilding(ZoneType.Yellow);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ignoreFirstInput = false;
            isBuilding = true;
            StartZoneBuilding(ZoneType.Orange);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ignoreFirstInput = false;
            isBuilding = true;
            StartZoneBuilding(ZoneType.Brown);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ignoreFirstInput = false;
            isBuilding = true;
            StartZoneBuilding(ZoneType.Purple);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ignoreFirstInput = false;
            isBuilding = true;
            StartZoneBuilding(ZoneType.Red);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ignoreFirstInput = false;
            isBuilding = true;
            StartZoneBuilding(ZoneType.Blue);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ignoreFirstInput = false;
            isBuilding = true;
            StartZoneBuilding(ZoneType.Erase);
        }

        // Debug building
        if (Input.GetKeyDown(KeyCode.B))
        {
            //BuildZoneStructure(zoneType, 5);
        }
    }

    /// <summary>
    /// Starts the zone building system
    /// </summary>
    /// <param name="zone">The zone to be built</param>
    public void StartZoneBuilding(ZoneType zone)
    {
        zoneType = zone;
        switch (zone)
        {
            case ZoneType.Green:
                selectedTile = tile_Green;
                break;

            case ZoneType.Yellow:
                selectedTile = tile_Yellow;
                break;

            case ZoneType.Orange:
                selectedTile = tile_Orange;
                break;

            case ZoneType.Brown:
                selectedTile = tile_Brown;
                break;

            case ZoneType.Purple:
                selectedTile = tile_Purple;
                break;

            case ZoneType.Red:
                selectedTile = tile_Red;
                break;

            case ZoneType.Blue:
                selectedTile = tile_Blue;
                break;

            default:
                selectedTile = null;
                break;
        }
    }

    /// <summary>
    /// A function that allow buttons to call StartZoneBuilding
    /// </summary>
    /// <param name="zoneID">Zone ID is the same as their keyboard input (ex: Green = 1)</param>
    public void ButtonStartZoneBuilding(int zoneID)
    {
        ignoreFirstInput = true;
        switch (zoneID)
        {
            case 1:
                StartZoneBuilding(ZoneType.Green);
                break;

            case 2:
                StartZoneBuilding(ZoneType.Yellow);
                break;

            case 3:
                StartZoneBuilding(ZoneType.Orange);
                break;

            case 4:
                StartZoneBuilding(ZoneType.Brown);
                break;

            case 5:
                StartZoneBuilding(ZoneType.Purple);
                break;

            case 6:
                StartZoneBuilding(ZoneType.Red);
                break;

            case 7:
                StartZoneBuilding(ZoneType.Blue);
                break;

            default:
                StartZoneBuilding(ZoneType.Erase);
                break;
        }
    }

    /// <summary>
    /// Perform a raycast on mouse location and return the position hit
    /// </summary>
    /// <returns>A Vector3 Position of hit location</returns>
    public Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }    
    }

    /// <summary>
    /// Snaps a world position to a grid position
    /// </summary>
    /// <param name="pos">Vector3 world position</param>
    /// <returns>Vector3 grid position</returns>
    public Vector3 WorldPosToGridPos(Vector3 pos)
    {
        Vector3Int gridPos = gridLayout.WorldToCell(pos);
        pos = grid.CellToWorld(gridPos);
        return pos;
    }


    // Taken from https://forum.unity.com/threads/tilemap-boxfill-is-horrible.502864/ by shawnblais Oct 11, 2012
    /// <summary>
    /// Fills the tilemap with a box using the given start and end position as the edges of a box
    /// </summary>
    /// <param name="map">The tilemap to be filled</param>
    /// <param name="tile">The tile used to fill the tilemap</param>
    /// <param name="start">The starting point of a box</param>
    /// <param name="end">The end point of a box</param>
    public void BoxFill(Tilemap map, TileBase tile, Vector3Int start, Vector3Int end)
    {
        //Determine directions on X and Y axis
        var xDir = start.x < end.x ? 1 : -1;
        var yDir = start.y < end.y ? 1 : -1;
        //How many tiles on each axis?
        int xCols = 1 + Mathf.Abs(start.x - end.x);
        int yCols = 1 + Mathf.Abs(start.y - end.y);
        //Start painting
        for (var x = 0; x < xCols; x++)
        {
            for (var y = 0; y < yCols; y++)
            {
                var tilePos = start + new Vector3Int(x * xDir, y * yDir, 0);
                map.SetTile(tilePos, tile);
                if (tile == null)
                {
                    buildingManager.RemoveBuilding(tilePos);
                    occupiedTiles.Remove(tilePos);
                }
            }
        }
    }

    public void BoxFill(Tilemap map, TileBase tile, Vector3 start, Vector3 end)
    {
        BoxFill(map, tile, map.WorldToCell(start), map.WorldToCell(end));
    }

    /// <summary>
    /// Moves the preview quad to the start position and changes color according to selected tile
    /// </summary>
    /// <param name="start">Starting point</param>
    /// <param name="zone">ZoneType</param>
    private void StartPreviewQuad(Vector3 start, ZoneType zone)
    {
        Color color;
        switch (zone)
        {
            case ZoneType.Green:
                color = new Color32(0, 117, 0, 200);
                break;

            case ZoneType.Yellow:
                color = new Color32(255, 255, 0, 200);
                break;

            case ZoneType.Orange:
                color = new Color32(255, 130, 0, 200);
                break;

            case ZoneType.Brown:
                color = new Color32(93, 52, 24, 200);
                break;

            case ZoneType.Purple:
                color = new Color32(227, 0, 227, 200);
                break;

            case ZoneType.Red:
                color = new Color32(255, 0, 0, 200);
                break;

            case ZoneType.Blue:
                color = new Color32(125, 130, 255, 200);
                break;

            default:
                color = new Color32(255, 255, 255, 200);
                break;
        }

        previewQuad.SetActive(true);
        previewQuad.transform.position = new Vector3(start.x, 0.02f, start.z);
        previewQuad.GetComponentInChildren<Renderer>().sharedMaterial.color = color;
    }

    /// <summary>
    /// Resizes the preview quad to fit the given position
    /// </summary>
    /// <param name="start_">Starting point of quad</param>
    /// <param name="end_">End point of quad</param>
    private void ResizePreviewQuad(Vector3 start_, Vector3 end_)
    {
        Vector3Int start = tilemap.WorldToCell(start_);
        Vector3Int end = tilemap.WorldToCell(end_);

        var xDir = start.x < end.x ? 1 : -1;
        var yDir = start.y < end.y ? 1 : -1;

        int xCols = 1 + Mathf.Abs(start.x - end.x);
        int yCols = 1 + Mathf.Abs(start.y - end.y);

        Vector3 tempPos = new Vector3(mouse_down.x, 0.02f, mouse_down.z);

        if (xDir < 0)
        {
            tempPos = tempPos + new Vector3(-xDir, 0, 0);
        }
        if (yDir < 0)
        {
            tempPos = tempPos + new Vector3(0, 0, -yDir);
        }

        previewQuad.transform.position = tempPos;
        previewQuad.transform.localScale = new Vector3(xCols * xDir, 1, yCols * yDir);
    }

    /// <summary>
    /// Finds an unoccupied tile of the given ZoneType, returns Vector3.zero if not found
    /// </summary>
    /// <param name="zone"></param>
    /// <returns>Vector3 of the empty tile</returns>
    public Vector3 GetBuildingPos(ZoneType zone)
    {
        Vector3Int start = new Vector3Int(-tilemapSize / 2, -tilemapSize / 2, 0);
        Vector3Int end = new Vector3Int(tilemapSize / 2, tilemapSize / 2, 0);

        int xCols = 1 + Mathf.Abs(start.x - end.x);
        int yCols = 1 + Mathf.Abs(start.y - end.y);

        for (var x = 0; x < xCols; x++)
        {
            for (var y = 0; y < yCols; y++)
            {
                var tilePos = start + new Vector3Int(x, y, 0);

                TileBase tile;
                switch (zone)
                {
                    case ZoneType.Green:
                        tile = tile_Green;
                        break;

                    case ZoneType.Yellow:
                        tile = tile_Yellow;
                        break;

                    case ZoneType.Orange:
                        tile = tile_Orange;
                        break;

                    case ZoneType.Brown:
                        tile = tile_Brown;
                        break;

                    case ZoneType.Purple:
                        tile = tile_Purple;
                        break;

                    case ZoneType.Red:
                        tile = tile_Red;
                        break;

                    case ZoneType.Blue:
                        tile = tile_Blue;
                        break;

                    default:
                        return Vector3.zero;
                }

                if (tilemap.GetTile(tilePos) == tile && !occupiedTiles.Contains(tilePos))
                {
                    occupiedTiles.Add(tilePos);
                    return tilemap.CellToWorld(tilePos);
                }
            }
        }
        return Vector3.zero;
    }

    public ZoneType GetZoneType()
    {
        return zoneType;
    }
}
