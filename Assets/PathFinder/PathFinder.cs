using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    public Vector2Int StartCoords { get { return startCoords; } }
    [SerializeField] Vector2Int endCoords;
    public Vector2Int EndCoords { get { return endCoords; } }
    Node currNode;
    Node startNode;
    Node endNode;
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoords];
            endNode = grid[endCoords];
        }
    }

    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        gridManager.RefreshNodes();
        BFS();
        return FindPath();
    }

    void BFS()
    {
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startNode.coordinates, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currNode = frontier.Dequeue();
            currNode.isExplored = true;
            ExploreNeighbors();
            if (currNode.coordinates == endNode.coordinates)
            {
                isRunning = false;
            }
        }
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbor = currNode.coordinates + direction;
            if (grid.ContainsKey(neighbor))
            {
                neighbors.Add(grid[neighbor]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.prev = currNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    List<Node> FindPath()
    {
        List<Node> path = new List<Node>();
        if (currNode.coordinates != endNode.coordinates)
        {
            return null;
        }
        while (currNode.prev != null)
        {
            path.Add(currNode);
            currNode.isPath = true;
            currNode = currNode.prev;
        }
        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool prevState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = prevState;

            if (newPath == null)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public event Action RecalculatePathRequested;
    public void RequestRecalculatePath()
    {
        RecalculatePathRequested?.Invoke();
    }
}
