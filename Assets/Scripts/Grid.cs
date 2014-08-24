using System.IO;
using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public GameObject GridTile;
    public int Width = 10;
    public int Height = 10;
    public float GridUnitSize;
    public Color GridSelectionColor = Color.blue;
    public Unit[,] Cells;

    private BoxCollider _gridCollider;
    private GameController _gc;

    void Awake()
    {
        Cells = new Unit[Width, Height];

        _gridCollider = GetComponent<BoxCollider>();
        _gc = GameController.Get();

        CreateGrid();
    }

    void Update()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().color = Color.white;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            var gridPos = WorldToGrid(hit.point);
            var gridX = (int) gridPos.x;
            var gridY = (int) gridPos.y;

            SetCellColor(gridPos, GridSelectionColor);

            if (Cells[gridX, gridY] != null && Input.GetButtonDown("Select"))
            {
                _gc.SelectedUnit = Cells[gridX, gridY];
                _gc.SelectedUnit.EnableBoundingBox();
            }
            else if (_gc.SelectedUnit != null)
            {
                var path = AStar.FindPath(Cells, WorldToGrid(_gc.SelectedUnit.transform.position), gridPos);
                while (path.Count > _gc.SelectedUnit.CellMoveCount)
                    path.RemoveAt(path.Count - 1);

                foreach (var cell in path)
                {
                    SetCellColor(cell, Color.red);
                }

                if (Input.GetButtonDown("Action"))
                {
                    _gc.SelectedUnit.MoveTo(path);
                }
            }
        }
    }

    public Vector3 WorldToGrid(Vector3 grid)
    {
        var worldPos = grid - transform.position - new Vector3(GridUnitSize / 2, GridUnitSize / 2, 0);
        var gridX = Mathf.FloorToInt(worldPos.x / GridUnitSize) + 1;
        var gridY = Mathf.FloorToInt(worldPos.y / GridUnitSize) + 1;

        return new Vector3(gridX, gridY, 0);
    }

    public Vector3 GridToWorld(Vector2 grid)
    {
        var worldPos = new Vector3(grid.x, grid.y) * GridUnitSize;
        worldPos += transform.position;// + new Vector3(GridUnitSize / 2, GridUnitSize / 2, 0);

        return worldPos;
    }

    public void SetCellColor(Vector2 pos, Color color)
    {
        transform.Find("Tile_" + pos.x.ToString() + ";" + pos.y.ToString()).GetComponent<SpriteRenderer>().color = color;
    }

    private void CreateGrid()
    {
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                var tile = Instantiate(GridTile, new Vector3(i*GridUnitSize, j*GridUnitSize, 0), Quaternion.identity) as GameObject;
                tile.transform.parent = transform;
                tile.name = "Tile_" + i.ToString() + ";" + j.ToString();
            }
        }

        _gridCollider.center = new Vector3((Width*GridUnitSize - 1)/2, (Height*GridUnitSize - 1)/2, 0.5f);
        _gridCollider.size   = new Vector3(Width*GridUnitSize, Height*GridUnitSize, 1);
    }

    public static Grid Get()
    {
        return GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
    }
}