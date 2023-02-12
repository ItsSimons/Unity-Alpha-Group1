using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZoneManager : MonoBehaviour
{
    public enum ZoneType {Green, Yellow, Orange, Brown, Purple, Red, Blue, Erase, Structure, Road};

    [SerializeField] BuildingManager buildingManager;
    [SerializeField] PopulationManager populationManager;
    [SerializeField] CurrencyManager currencyManager;
    [SerializeField] VibesGrid vibeManager;
    [SerializeField] MapGen terrainGenerator;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private GameObject previewQuad;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private int tilemapSize;

    private TileBase tile_Green;
    private TileBase tile_Yellow;
    private TileBase tile_Orange;
    private TileBase tile_Brown;
    private TileBase tile_Purple;
    private TileBase tile_Red;
    private TileBase tile_Blue;

    private TileBase tile_Inactive_Green;
    private TileBase tile_Inactive_Yellow;
    private TileBase tile_Inactive_Orange;
    private TileBase tile_Inactive_Brown;
    private TileBase tile_Inactive_Purple;
    private TileBase tile_Inactive_Red;
    private TileBase tile_Inactive_Blue;

    private TileBase tile_Structure;
    private TileBase tile_Water;
    private TileBase tile_Lava;
    private TileBase tile_Rock;
    private TileBase tile_Res;
    private TileBase tile_Road;

    private ZoneType selectedZone;
    private TileBase selectedTile;
    private BuildingManager.Structures selectedStructure;

    private Vector3 mouse_down;
    private Vector3 mouse_up;
    private Vector3 mouse_drag;
    private Vector3 quad_end;

    private List<Vector3Int> occupiedTiles;

    [SerializeField] private bool isHeaven;
    private bool isZone;
    private bool isBuildingZone;
    private bool isBuildingStructure;
    private bool isBuildingRoad;
    private bool ignoreFirstInput;

    private void Awake()
    {
        grid = gridLayout.gameObject.GetComponent<Grid>();
        previewQuad.SetActive(false);
        occupiedTiles = new List<Vector3Int>();
        isBuildingZone = false;
        isBuildingStructure = false;
        isBuildingRoad = false;
        ignoreFirstInput = false;

        tile_Green = Resources.Load<TileBase>("Tile Palette/Tile_Green");
        tile_Yellow = Resources.Load<TileBase>("Tile Palette/Tile_Yellow");
        tile_Orange = Resources.Load<TileBase>("Tile Palette/Tile_Orange");
        tile_Brown = Resources.Load<TileBase>("Tile Palette/Tile_Brown");
        tile_Purple = Resources.Load<TileBase>("Tile Palette/Tile_Purple");
        tile_Red = Resources.Load<TileBase>("Tile Palette/Tile_Red");
        tile_Blue = Resources.Load<TileBase>("Tile Palette/Tile_Blue");

        tile_Inactive_Green = Resources.Load<TileBase>("Tile Palette/Tile_Inactive_Green");
        tile_Inactive_Yellow = Resources.Load<TileBase>("Tile Palette/Tile_Inactive_Yellow");
        tile_Inactive_Orange = Resources.Load<TileBase>("Tile Palette/Tile_Inactive_Orange");
        tile_Inactive_Brown = Resources.Load<TileBase>("Tile Palette/Tile_Inactive_Brown");
        tile_Inactive_Purple = Resources.Load<TileBase>("Tile Palette/Tile_Inactive_Purple");
        tile_Inactive_Red = Resources.Load<TileBase>("Tile Palette/Tile_Inactive_Red");
        tile_Inactive_Blue = Resources.Load<TileBase>("Tile Palette/Tile_Inactive_Blue");

        tile_Structure = Resources.Load<TileBase>("Tile Palette/Tile_Structure");
        tile_Water = Resources.Load<TileBase>("Tile Palette/Tile_Water");
        tile_Lava = Resources.Load<TileBase>("Tile Palette/Tile_Lava");
        tile_Rock = Resources.Load<TileBase>("Tile Palette/Tile_Rock");
        tile_Res = Resources.Load<TileBase>("Tile Palette/Tile_Res");
        tile_Road = Resources.Load<TileBase>("Tile Palette/Tile_Road");
    }

    private void Start()
    {
        GenerateRandomTerrain();
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
        if (isBuildingZone || isBuildingStructure)
        {
            MovePreviewQuad(WorldPosToGridPos(GetMouseWorldPos()), selectedZone);
        }

        // Mouse down
        if (Input.GetMouseButtonDown(0))
        {
            mouse_down = WorldPosToGridPos(GetMouseWorldPos());
        }

        // Mouse hold
        else if (Input.GetMouseButton(0))
        {
            mouse_drag = WorldPosToGridPos(GetMouseWorldPos());

            if (isBuildingRoad && isBuildingZone)
            {
                ResizePreviewQuad(mouse_down, ClampMouseToAxis(mouse_down, mouse_drag));
            }
            else if (isBuildingZone)
            {
                ResizePreviewQuad(mouse_down, mouse_drag);
            }
        }

        // Mouse up
        else if (Input.GetMouseButtonUp(0))
        {
            mouse_up = WorldPosToGridPos(GetMouseWorldPos());

            if (isBuildingZone)
            {
                if (isBuildingRoad)
                {
                    BoxFill(tilemap, selectedTile, mouse_down, ClampMouseToAxis(mouse_down, mouse_up));
                    isBuildingRoad = false;
                }
                else
                {
                    BoxFill(tilemap, selectedTile, mouse_down, mouse_up);
                }
                
                populationManager.SetZoneNotFull(selectedZone);
                previewQuad.transform.localScale = Vector3.one;
                previewQuad.SetActive(false);
                isBuildingZone = false;
            }
            else if (isBuildingStructure)
            {
                if (IsBoxEmpty(tilemap, mouse_up, mouse_up + quad_end))
                {
                    BoxFill(tilemap, selectedTile, mouse_up, mouse_up + quad_end);
                    CreateStructure(selectedStructure);
                }   

                previewQuad.transform.localScale = Vector3.one;
                previewQuad.SetActive(false);
                isBuildingStructure = false;
            }

            if (ignoreFirstInput)
            {
                ignoreFirstInput = false;
                if (isZone)
                {
                    isBuildingZone = true;
                    isBuildingStructure = false;
                }
                else
                {
                    isBuildingStructure = true;
                    isBuildingZone = false;
                }
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
        // R = Road
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ignoreFirstInput = false;
            isBuildingZone = true;
            isBuildingStructure = false;
            isZone = true;
            StartZoneBuilding(ZoneType.Green);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ignoreFirstInput = false;
            isBuildingZone = true;
            isBuildingStructure = false;
            isZone = true;
            StartZoneBuilding(ZoneType.Yellow);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ignoreFirstInput = false;
            isBuildingZone = true;
            isBuildingStructure = false;
            isZone = true;
            StartZoneBuilding(ZoneType.Orange);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ignoreFirstInput = false;
            isBuildingZone = true;
            isBuildingStructure = false;
            isZone = true;
            StartZoneBuilding(ZoneType.Brown);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ignoreFirstInput = false;
            isBuildingZone = true;
            isBuildingStructure = false;
            isZone = true;
            StartZoneBuilding(ZoneType.Purple);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ignoreFirstInput = false;
            isBuildingZone = true;
            isBuildingStructure = false;
            isZone = true;
            StartZoneBuilding(ZoneType.Red);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ignoreFirstInput = false;
            isBuildingZone = true;
            isBuildingStructure = false;
            isZone = true;
            StartZoneBuilding(ZoneType.Blue);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ignoreFirstInput = false;
            isBuildingZone = true;
            isBuildingStructure = false;
            isZone = true;
            StartZoneBuilding(ZoneType.Erase);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ignoreFirstInput = false;
            isBuildingZone = true;
            isBuildingRoad = true;
            isBuildingStructure = false;
            isZone = true;
            StartZoneBuilding(ZoneType.Road);
        }
    }

    private void GenerateRandomTerrain()
    {
        List<Vector3Int> terrain_list = terrainGenerator.GenerateMapAsVectors();
        Vector3Int pos;

        foreach (Vector3Int tile in terrain_list)
        {
            switch (tile.z)
            {
                case 1:
                    pos = new Vector3Int(tile.x - 50, tile.y - 50, 0);
                    tilemap.SetTile(pos, tile_Rock);
                    occupiedTiles.Add(pos);
                    buildingManager.CreateRandomRock(grid.CellToWorld(pos));
                    vibeManager.karmaChange1x1(tile.x, tile.y, 1);
                    break;

                case 2:
                    if (isHeaven)
                    {
                        tilemap.SetTile(new Vector3Int(tile.x - 50, tile.y - 50, 0), tile_Water);
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(tile.x - 50, tile.y - 50, 0), tile_Lava);
                    }
                    break;

                case 3:
                    pos = new Vector3Int(tile.x - 50, tile.y - 50, 0);
                    tilemap.SetTile(pos, tile_Res);

                    if (!occupiedTiles.Contains(pos))
                    {
                        buildingManager.CreateKarmaAnchor(grid.CellToWorld(pos));

                        occupiedTiles.Add(pos);
                        occupiedTiles.Add(pos + new Vector3Int(1, 0, 0));
                        occupiedTiles.Add(pos + new Vector3Int(2, 0, 0));
                        occupiedTiles.Add(pos + new Vector3Int(0, 1, 0));
                        occupiedTiles.Add(pos + new Vector3Int(1, 1, 0));
                        occupiedTiles.Add(pos + new Vector3Int(2, 1, 0));
                        occupiedTiles.Add(pos + new Vector3Int(0, 2, 0));
                        occupiedTiles.Add(pos + new Vector3Int(1, 2, 0));
                        occupiedTiles.Add(pos + new Vector3Int(2, 2, 0));
                    }

                    break;
            }
        }
    }

    /// <summary>
    /// Starts the zone building system
    /// </summary>
    /// <param name="zone">The zone to be built</param>
    private void StartZoneBuilding(ZoneType zone)
    {
        selectedZone = zone;
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

            case ZoneType.Erase:
                selectedTile = null;
                break;

            case ZoneType.Road:
                selectedTile = tile_Road;
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
        isZone = true;
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
    /// A function that allows button to start building road
    /// </summary>
    private void ButtonStartRoadBuilding()
    {
        ignoreFirstInput = true;
        isBuildingZone = true;
        isBuildingRoad = true;
        isBuildingStructure = false;
        isZone = true;
        StartZoneBuilding(ZoneType.Road);
    }

    /// <summary>
    /// Checks if the mouse is in the plane this script is attached to
    /// </summary>
    /// <returns>True if it is, false if it is not</returns>
    private bool IsMouseInThisPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.tag == "Heaven" && isHeaven)
            {
                Debug.Log("Heaven");
                return true;
            }
            else if (hit.transform.tag == "Hell" && !isHeaven)
            {
                Debug.Log("Hell");
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Clamps the end valye to the most dominant axis relative to the start
    /// </summary>
    /// <param name="start">Start position</param>
    /// <param name="end">End position</param>
    /// <returns>Clamped position</returns>
    private Vector3 ClampMouseToAxis(Vector3 start, Vector3 end)
    {
        float xDiff = Mathf.Abs(Mathf.Abs(end.x) - Mathf.Abs(start.x));
        float yDiff = Mathf.Abs(Mathf.Abs(end.z) - Mathf.Abs(start.z));

        if (xDiff < yDiff)
        {
            // Lock to Z
            end.x = start.x;
            return end;
        }
        else
        {
            // Lock to X
            end.z = start.z;
            return end;
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

    // Based on https://forum.unity.com/threads/tilemap-boxfill-is-horrible.502864/ by shawnblais Oct 11, 2012
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

                if (CanTileBeReplaced(map.GetTile(tilePos)) && IsTileWithinBounds(tilePos))
                {
                    // Existing Tile at tilePos can be replaced and tilePos is within the map
                    if (IsTileBuildable(tile))
                    {
                        // Current tile is a virtue/sin
                        if (IsRoadNearby(tilePos))
                        {
                            // There is a road in a 3 tile radius near the tilePos
                            if (map.GetTile(tilePos) != tile_Road)
                            {
                                // Existing tile at tilePos is not a road
                                currencyManager.TransactionTile(xCols * yCols);
                                map.SetTile(tilePos, tile);
                            }
                        }
                        else
                        {
                            // There is no road nearby, create inactive tile instead
                            map.SetTile(tilePos, GetInactiveTile(selectedZone));
                        }
                    }
                    else
                    {
                        // Current tile is not a virtue/sin
                        if (tile == tile_Road)
                        {
                            // Activate tiles around road
                            ActivateNearbyTiles(tilePos);
                        }
                        map.SetTile(tilePos, tile);
                    }

                    if (tile == null)
                    {
                        // Tile is Eraser, remove structures on the tile
                        buildingManager.RemoveBuilding(tilePos);
                        occupiedTiles.Remove(tilePos);
                    }
                    else if (tile == tile_Structure)
                    {
                        // Tile is Structure, mark tile as occupied
                        occupiedTiles.Add(tilePos);
                    }
                }
            }
        }
    }

    public void BoxFill(Tilemap map, TileBase tile, Vector3 start, Vector3 end)
    {
        BoxFill(map, tile, map.WorldToCell(start), map.WorldToCell(end));
    }

    /// <summary>
    /// Checks if tile can be replaced
    /// </summary>
    /// <param name="tile">TileBase of the tile</param>
    /// <returns>True if can be replaced, False if cannot</returns>
    private bool CanTileBeReplaced(TileBase tile)
    {
        if (tile == tile_Water || tile == tile_Rock || tile == tile_Lava || tile == tile_Res)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the tile is a virtue/sin
    /// </summary>
    /// <param name="tile">Tilebase of the tile</param>
    /// <returns>True if it is a virtue/sin, False otherwise</returns>
    private bool IsTileBuildable(TileBase tile)
    {
        if (tile == tile_Green || tile == tile_Yellow || tile == tile_Orange || tile == tile_Brown || tile == tile_Purple || tile == tile_Red || tile == tile_Blue)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get the inactive tile counterpart of a zone
    /// </summary>
    /// <param name="zone">The zone</param>
    /// <returns>TileBase of the inactive tile of the respective zone</returns>
    private TileBase GetInactiveTile(ZoneType zone)
    {
        switch (zone)
        {
            case ZoneType.Green:
                return tile_Inactive_Green;

            case ZoneType.Yellow:
                return tile_Inactive_Yellow;

            case ZoneType.Orange:
                return tile_Inactive_Orange;

            case ZoneType.Brown:
                return tile_Inactive_Brown;

            case ZoneType.Purple:
                return tile_Inactive_Purple;

            case ZoneType.Red:
                return tile_Inactive_Red;

            case ZoneType.Blue:
                return tile_Inactive_Blue;

            default:
                return null;
        }
    }

    /// <summary>
    /// Checks if the tile is within bounds
    /// </summary>
    /// <param name="tilePos">Position of the tile</param>
    /// <returns>True if within bounds, False if outside</returns>
    private bool IsTileWithinBounds(Vector3Int tilePos)
    {
        if (Mathf.Abs(tilePos.x) < 50 && Mathf.Abs(tilePos.y) < 50)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Chekc if box between given positions is buildable
    /// </summary>
    /// <param name="map">Tilemap</param>
    /// <param name="_start">Starting point of box</param>
    /// <param name="_end">End point of box</param>
    /// <returns>True if empty, False if not</returns>
    private bool IsBoxEmpty(Tilemap map, Vector3 _start, Vector3 _end)
    {
        Vector3Int start = map.WorldToCell(_start);
        Vector3Int end = map.WorldToCell(_end);

        var xDir = start.x < end.x ? 1 : -1;
        var yDir = start.y < end.y ? 1 : -1;

        int xCols = 1 + Mathf.Abs(start.x - end.x);
        int yCols = 1 + Mathf.Abs(start.y - end.y);

        for (var x = 0; x < xCols; x++)
        {
            for (var y = 0; y < yCols; y++)
            {
                var tilePos = start + new Vector3Int(x * xDir, y * yDir, 0);
                if (occupiedTiles.Contains(tilePos) || !IsTileWithinBounds(tilePos) || !CanTileBeReplaced(map.GetTile(tilePos)))
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Moves the preview quad to the start position and changes color according to selected tile
    /// </summary>
    /// <param name="start">Starting point</param>
    /// <param name="zone">ZoneType</param>
    private void MovePreviewQuad(Vector3 start, ZoneType zone)
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

            case ZoneType.Structure:
                color = new Color32(62, 57, 79, 200);
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
    /// Creates a preview quad for structure placement
    /// </summary>
    /// <param name="size">Size of the preview quad in tiles</param>
    /// <param name="structure">The structure to be placed</param>
    public void CreatePreviewQuadOfSize(int size, BuildingManager.Structures structure)
    {
        ignoreFirstInput = true;
        isZone = false;

        previewQuad.transform.localScale = new Vector3(size, 1, size);
        quad_end = new Vector3(size - 1, 0, size - 1);
        selectedZone = ZoneType.Structure;
        selectedTile = tile_Structure;
        selectedStructure = structure;
    }

    /// <summary>
    /// Finds a tile of the given ZoneType and size
    /// </summary>
    /// <param name="zone">ZoneType of the tile/param>
    /// <param name="size">The area of the tile</param>
    /// <returns>A Vector3 world position of the most negative corner of the tile</returns>
    public Vector3 FindTileOfSize(ZoneType zone, int size)
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

                Vector3 foundPos;
                switch (size)
                {
                    case 1:
                        foundPos = Find1x1Tile(tilePos, tile);
                        break;

                    case 2:
                        foundPos = Find2x2Tile(tilePos, tile);
                        break;

                    default:
                        foundPos = Vector3.zero;
                        break;
                }

                if (foundPos != Vector3.zero)
                {
                    return foundPos;
                }
            }
        }
        return Vector3.zero;
    }

    private Vector3 Find1x1Tile(Vector3Int tilePos, TileBase tile)
    {
        if (tilemap.GetTile(tilePos) == tile && !occupiedTiles.Contains(tilePos))
        {
            occupiedTiles.Add(tilePos);
            return tilemap.CellToWorld(tilePos);
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 Find2x2Tile(Vector3Int tilePos, TileBase tile)
    {
        List<Vector3Int> tileList = new List<Vector3Int>();
        tileList.Add(tilePos);
        tileList.Add(tilePos + new Vector3Int(1, 0, 0));
        tileList.Add(tilePos + new Vector3Int(0, 1, 0));
        tileList.Add(tilePos + new Vector3Int(1, 1, 0));

        int tileFound = 0;
        foreach (Vector3Int tempTilePos in tileList)
        {
            if (tilemap.GetTile(tempTilePos) == tile && !occupiedTiles.Contains(tempTilePos))
            {
                tileFound += 1;
            }

            if (tileFound == 4)
            {
                occupiedTiles.AddRange(tileList);
                return tilemap.CellToWorld(tilePos);
            }
        }
        return Vector3.zero;
    }

    /// <summary>
    /// Returns the ZoneType of the tile at the position
    /// </summary>
    /// <param name="pos">Vector3 world pos</param>
    /// <returns>ZoneType of the tile at position</returns>
    public ZoneType GetZoneTypeAtTile(Vector3 pos)
    {
        Vector3Int tilePos = grid.WorldToCell(pos);
        TileBase tile = tilemap.GetTile(tilePos);
        
        if (tile == tile_Green)
        {
            return ZoneType.Green;
        }
        else if (tile == tile_Yellow)
        {
            return ZoneType.Yellow;
        }
        else if (tile == tile_Orange)
        {
            return ZoneType.Orange;
        }
        else if (tile == tile_Brown)
        {
            return ZoneType.Brown;
        }
        else if (tile == tile_Purple)
        {
            return ZoneType.Purple;
        }
        else if (tile == tile_Red)
        {
            return ZoneType.Red;
        }
        else if (tile == tile_Blue)
        {
            return ZoneType.Blue;
        }
        else if (tile == tile_Structure)
        {
            return ZoneType.Structure;
        }
        else
        {
            return ZoneType.Erase;
        }
    }

    /// <summary>
    /// Checks if there is a road in a 3 tile radius around the tilePos
    /// </summary>
    /// <param name="tilePos">Position of tile to be checked</param>
    /// <returns>True if there is a road, False if there is no road</returns>
    private bool IsRoadNearby(Vector3Int tilePos)
    {
        for (int x = -3; x <= 3; x++)
        {
            for (int y = -3; y <= 3; y++)
            {
                if (tilemap.GetTile(tilePos + new Vector3Int(x, y, 0)) == tile_Road)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Turns any inactive tile in a 3 tile radius from the tilePos into their active counterpart
    /// </summary>
    /// <param name="tilePos">Position of the tile</param>
    private void ActivateNearbyTiles(Vector3Int tilePos)
    {
        for (int x = -3; x <= 3; x++)
        {
            for (int y = -3; y <= 3; y++)
            {
                Vector3Int tempTilePos = tilePos + new Vector3Int(x, y, 0);

                TileBase currentTileBase = tilemap.GetTile(tempTilePos);

                if (currentTileBase == tile_Inactive_Green)
                {
                    tilemap.SetTile(tempTilePos, tile_Green);
                }
                else if (currentTileBase == tile_Inactive_Yellow)
                {
                    tilemap.SetTile(tempTilePos, tile_Yellow);
                }
                else if (currentTileBase == tile_Inactive_Orange)
                {
                    tilemap.SetTile(tempTilePos, tile_Orange);
                }
                else if (currentTileBase == tile_Inactive_Brown)
                {
                    tilemap.SetTile(tempTilePos, tile_Brown);
                }
                else if (currentTileBase == tile_Inactive_Purple)
                {
                    tilemap.SetTile(tempTilePos, tile_Purple);
                }
                else if (currentTileBase == tile_Inactive_Red)
                {
                    tilemap.SetTile(tempTilePos, tile_Red);
                }
                else if (currentTileBase == tile_Inactive_Blue)
                {
                    tilemap.SetTile(tempTilePos, tile_Blue);
                }
            }
        }
    }

    /// <summary>
    /// Checks if the current plane is Heaven
    /// </summary>
    /// <returns>True if Heaven, False if Hell</returns>
    public bool IsThisHeaven()
    {
        return isHeaven;
    }

    /// <summary>
    /// Creates a structure at mouse location
    /// </summary>
    /// <param name="structure">Structure to be built</param>
    private void CreateStructure(BuildingManager.Structures structure)
    {
        currencyManager.TransactionStructures(structure);
        switch (structure)
        {
            case BuildingManager.Structures.Gate:
                buildingManager.InstatiateGate(new Vector3(mouse_up.x, 0, mouse_up.z));
                break;
        }
    }
}
