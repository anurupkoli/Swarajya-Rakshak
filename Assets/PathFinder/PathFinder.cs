using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    [SerializeField] Vector2Int endCoords;
    Node currNode;
    Node startNode;
    Node endNode;
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> forntier = new Queue<Node>();
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null){
            grid = gridManager.Grid;
        }
    }

    void Start(){  
        startNode = grid[startCoords];
        endNode = grid[endCoords];
        BFS();
    }

    void ExploreNeighbors(){
        List<Node> neighbors = new List<Node>();
        foreach(Vector2Int direction in directions){
            Vector2Int neighbor = currNode.coordinates + direction;
            if(grid.ContainsKey(neighbor)){
                neighbors.Add(grid[neighbor]);
            }
        }

        foreach(Node neighbor in neighbors){
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable){
                neighbor.prev = currNode;
                reached.Add(neighbor.coordinates, neighbor);
                forntier.Enqueue(neighbor);
            }
        }
    }

    void BFS(){
        bool isRunning = true;

        forntier.Enqueue(startNode);
        reached.Add(startNode.coordinates, startNode);

        while(forntier.Count > 0 && isRunning){
            currNode = forntier.Dequeue();
            currNode.isExplored = true;
            ExploreNeighbors();
            if(currNode.coordinates == endNode.coordinates){
                isRunning = false;
            }
        }
    }

    List<Node> FindPath(){
        List<Node> path = new List<Node>();
        while(currNode.prev != null){
            path.Add(currNode);
            currNode.isPath = true;
            currNode = currNode.prev;
        }
        path.Reverse();
        return path;
    }
}
