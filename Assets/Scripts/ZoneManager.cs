using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZoneManager : MonoBehaviour
{
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap tilemap;
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

    private void Awake()
    {
        grid = gridLayout.gameObject.GetComponent<Grid>();
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
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouse_up = WorldPosToGridPos(GetMouseWorldPos());
            BoxFill(tilemap, selectedTile, mouse_down, mouse_up);
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
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTile = tile_Yellow;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedTile = tile_Orange;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedTile = tile_Brown;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedTile = tile_Purple;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedTile = tile_Red;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectedTile = tile_Blue;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectedTile = null;
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
            }
        }
    }

    public void BoxFill(Tilemap map, TileBase tile, Vector3 start, Vector3 end)
    {
        BoxFill(map, tile, map.WorldToCell(start), map.WorldToCell(end));
    }
}
