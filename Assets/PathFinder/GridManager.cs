using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSize = 10;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid{ get{return grid;}}

    void Awake(){
        CreateGrid();
    }

    public Vector2Int GetCoordinates(Vector3 position){
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);
        return coordinates;
    }

    public Vector3Int GetPosition(Vector2Int coordinates){
        Vector3Int position = new Vector3Int();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;
        return position;
    }

    public Node GetNode(Vector2Int coordinates){
        if(!grid.ContainsKey(coordinates)){return null;}
        return grid[coordinates];
    }

    public void BlockNode(Vector2Int coordinates){
        if(!grid.ContainsKey(coordinates)){return;}
        grid[coordinates].isWalkable = false;
    }

    public void RefreshNodes(){
        foreach(KeyValuePair<Vector2Int, Node> node in grid){
            node.Value.isExplored = false;
            node.Value.isPath = false;
            node.Value.prev = null;
        }
    }

    void CreateGrid(){
        for(int x=0; x<gridSize.x; x++){
            for(int y=0; y<gridSize.y; y++){
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}
