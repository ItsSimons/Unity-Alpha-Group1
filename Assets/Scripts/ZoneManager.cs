using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZoneManager : MonoBehaviour
{
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
    private int selectedTileID;

    private Vector3 mouse_down;
    private Vector3 mouse_up;
    private Vector3 mouse_drag;

    [SerializeField] private Transform strucutre_parent;
    [SerializeField] private GameObject structure_prefab;

    [SerializeField] private Sprite structure_Green;
    [SerializeField] private Sprite structure_Yellow;
    [SerializeField] private Sprite structure_Orange;
    [SerializeField] private Sprite structure_Brown;
    [SerializeField] private Sprite structure_Purple;
    [SerializeField] private Sprite structure_Red;
    [SerializeField] private Sprite structure_Blue;

    private List<Vector3Int> occupiedTiles;

    private void Awake()
    {
        grid = gridLayout.gameObject.GetComponent<Grid>();
        previewQuad.SetActive(false);
        occupiedTiles = new List<Vector3Int>();
    }

    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        // Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            mouse_down = WorldPosToGridPos(GetMouseWorldPos());

            StartPreviewQuad(mouse_down, selectedTileID);
        }
        else if (Input.GetMouseButton(0))
        {
            mouse_drag = WorldPosToGridPos(GetMouseWorldPos());

            ResizePreviewQuad(mouse_down, mouse_drag);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouse_up = WorldPosToGridPos(GetMouseWorldPos());

            BoxFill(tilemap, selectedTile, mouse_down, mouse_up);

            previewQuad.SetActive(false);
        }

        // Keybaord Input
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
            selectedTile = tile_Green;
            selectedTileID = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTile = tile_Yellow;
            selectedTileID = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedTile = tile_Orange;
            selectedTileID = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedTile = tile_Brown;
            selectedTileID = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedTile = tile_Purple;
            selectedTileID = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedTile = tile_Red;
            selectedTileID = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectedTile = tile_Blue;
            selectedTileID = 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectedTile = null;
            selectedTileID = 8;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            BuildZoneStructure(selectedTileID, 5);
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
        pos = grid.GetCellCenterWorld(gridPos);
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
                    RemoveStructure(tilePos);
                }
            }
        }
    }

    public void BoxFill(Tilemap map, TileBase tile, Vector3 start, Vector3 end)
    {
        BoxFill(map, tile, map.WorldToCell(start), map.WorldToCell(end));
    }

    /// <summary>
    /// Removes a structure on a given tile
    /// </summary>
    /// <param name="tilePos">Position of the tile</param>
    private void RemoveStructure(Vector3Int tilePos)
    {
        for (var i = 0; i < strucutre_parent.childCount; i++)
        {
            if ((int)strucutre_parent.GetChild(i).transform.position.x == tilePos.x && (int)strucutre_parent.GetChild(i).transform.position.z == tilePos.y)
            {
                Destroy(strucutre_parent.GetChild(i).gameObject);
                occupiedTiles.Remove(tilePos);
            }
        }
    }

    /// <summary>
    /// Moves the preview quad to the start position and changes color according to selected tile
    /// </summary>
    /// <param name="start">Starting point (mouse_down)</param>
    /// <param name="tile">TileID (SelectedTileID)</param>
    private void StartPreviewQuad(Vector3 start, int tile)
    {
        Color color;
        switch (tile)
        {
            case 1:
                color = new Color32(0, 117, 0, 200);
                break;

            case 2:
                color = new Color32(255, 255, 0, 200);
                break;

            case 3:
                color = new Color32(255, 130, 0, 200);
                break;

            case 4:
                color = new Color32(93, 52, 24, 200);
                break;

            case 5:
                color = new Color32(227, 0, 227, 200);
                break;

            case 6:
                color = new Color32(255, 0, 0, 200);
                break;

            case 7:
                color = new Color32(125, 130, 255, 200);
                break;

            default:
                color = new Color32(255, 255, 255, 200);
                break;
        }

        previewQuad.SetActive(true);
        previewQuad.transform.position = new Vector3(start.x - 0.5f, 0.02f, start.z - 0.5f);
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

        Vector3 tempPos = new Vector3(mouse_down.x - 0.5f, 0.02f, mouse_down.z - 0.5f);

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
    /// Creates a structure on an empty zone tile
    /// </summary>
    /// <param name="zoneID">The zone to be filled with structure</param>
    /// <param name="numberOfStructures">The number of structure to be placed</param>
    public void BuildZoneStructure(int zoneID, int numberOfStructures)
    {
        Vector3Int start = new Vector3Int(-tilemapSize/2, -tilemapSize / 2, 0);
        Vector3Int end = new Vector3Int(tilemapSize/2, tilemapSize/2, 0);

        int xCols = 1 + Mathf.Abs(start.x - end.x);
        int yCols = 1 + Mathf.Abs(start.y - end.y);

        for (var x = 0; x < xCols; x++)
        {
            for (var y = 0; y < yCols; y++)
            {
                var tilePos = start + new Vector3Int(x, y, 0);

                TileBase tile;
                Sprite sprite;
                switch (zoneID)
                {
                    case 1:
                        tile = tile_Green;
                        sprite = structure_Green;
                        break;

                    case 2:
                        tile = tile_Yellow;
                        sprite = structure_Yellow;
                        break;

                    case 3:
                        tile = tile_Orange;
                        sprite = structure_Orange;
                        break;

                    case 4:
                        tile = tile_Brown;
                        sprite = structure_Brown;
                        break;

                    case 5:
                        tile = tile_Purple;
                        sprite = structure_Purple;
                        break;

                    case 6:
                        tile = tile_Red;
                        sprite = structure_Red;
                        break;

                    default:
                        tile = tile_Blue;
                        sprite = structure_Blue;
                        break;
                }

                if (tilemap.GetTile(tilePos) == tile && numberOfStructures > 0 && !occupiedTiles.Contains(tilePos))
                {
                    GameObject newStructure = Instantiate(structure_prefab, tilemap.CellToWorld(tilePos), Quaternion.identity);
                    newStructure.transform.SetParent(strucutre_parent);
                    newStructure.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
                    numberOfStructures -= 1;
                    occupiedTiles.Add(tilePos);
                }
            }
        }
    }
}
