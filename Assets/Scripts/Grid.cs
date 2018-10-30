using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int _startWidth;
    [SerializeField] private int _startHeight;

    private GameObject[,] _grid;

    public GameObject[,] Array
    {
        get { return _grid; }
    }
    public int Width
    {
        get { return _grid.GetLength(0); }
    }
    public int Height
    {
        get { return _grid.GetLength(1); }
    }

    public GameObject this[int x, int y]
    {
        get { return GetNode(x, y); }
        set { SetNode(value, x, y); }
    }
    public GameObject this[Vector2Int pos]
    {
        get { return GetNode(pos); }
        set { SetNode(value, pos); }
    }

    private void Awake()
    {
        _grid = new GameObject[_startWidth, _startHeight];
        ResetGrid();
    }

    public GameObject GetNode(Vector2Int node)
    {
        return GetNode(node.x, node.y);
    }
    public GameObject GetNode(int x, int y)
    {
        if (x > Width-1 || y > Height-1 || x < 0 || y < 0)
            return null;
        return _grid[x, y];
    }

    public void SetNode(GameObject newNode, Vector2Int node)
    {
        SetNode(newNode, (int)node.x, (int)node.y);
    }
    public void SetNode(GameObject newNode, int x, int y)
    {
        if (x > Width-1 || y > Height-1) { return; }
        _grid[x, y] = newNode;
    }

    public Vector2Int FindObject(GameObject obj)
    {
        for (int x = 0; x < Array.GetLength(0); x++)
        {
            for (int y = 0; y < Array.GetLength(1); y++) {
                if (GetNode(x, y) == obj)
                    return new Vector2Int(x, y);
            }
        }
        return new Vector2Int(-1, -1);
    }

    public void ForEachNode(Func<GameObject, int, int, GameObject> doWithNode)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _grid[x, y] = doWithNode(_grid[x, y], x, y);
            }
        }
    }

    public void ResetGrid(GameObject node = null)
    {
        _grid = ResetGrid(_grid, node);
    }

    public void ResizeGrid(int width, int height, GameObject newNodes = null, bool keepOldValues = true)
    {
        var grid = new GameObject[width, height];
        grid = ResetGrid(grid, newNodes);
        if (!keepOldValues)
        {
            _grid = grid;
            return;
        }

        var resetX = width > _grid.GetLength(0) ? _grid.GetLength(0) : width;
        var resetY = height > _grid.GetLength(1) ? _grid.GetLength(1) : height;

        for (int fx = 0; fx < resetX; fx++)
        {
            for (int fy = 0; fy < resetY; fy++)
            {
                grid[fx, fy] = _grid[fx, fy];
            }
        }
        _grid = grid;
    }

    public Vector2Int[] GetNeighboursPositions(int x, int y, bool crosswise = true)
    {
        return GetNeighboursPositions(new Vector2Int(x, y), crosswise);
    }
    public Vector2Int[] GetNeighboursPositions(Vector2Int nodePosition, bool crosswise = true)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {

                if ((!crosswise && (y == 1 || y == -1) && (x == 1 || x == -1)) ||
                    x == 0 && y == 0)
                    continue;

                var position = new Vector2Int(x, y) + nodePosition;

                if (position.x > Width-1 || position.y > Height-1 || position.x < 0 || position.y < 0)
                    continue;

                neighbours.Add(position);
            }
        }
        return neighbours.ToArray();

    }

    public GameObject[] GetNeighbours(int x, int y, bool crosswise = true)
    {
        return GetNeighbours(new Vector2Int(x, y), crosswise);
    }
    public GameObject[] GetNeighbours(Vector2Int nodePosition, bool crosswise = true)
    {
        List<GameObject> neighbours = new List<GameObject>();
        var positions = GetNeighboursPositions(nodePosition, crosswise);
        for (int i = 0; i < positions.Length; i++)
        {
            neighbours.Add(GetNode(positions[i]));
        }
        return neighbours.ToArray();

    }

    public GameObject[] GetReletives(int x, int y, Vector2Int[] reletivesPosition)
    {
        return GetReletives(new Vector2Int(x, y), reletivesPosition);
    }
    public GameObject[] GetReletives(Vector2Int nodePosition, Vector2Int[] reletivesPosition)
    {
        var nodeX = nodePosition.x;
        var nodeY = nodePosition.y;

        var reletives = new List<GameObject>();

        for (int i = 0; i < reletivesPosition.Length; i++)
        {
            reletives.Add(GetNode(nodeX + reletivesPosition[i].x, nodeY + reletivesPosition[i].y));
        }

        return reletives.ToArray();
    }

    private GameObject[,] ResetGrid(GameObject[,] grid, GameObject node = null)
    {

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y] = node;
            }
        }
        return grid;
    }


}