using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] GameObject ballistaPrefab;
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlacable = false;
    public bool IsPlacable{get{return isPlacable;}}
    GridManager gridManager;

    TowerSpawner towerSpawner;
    public int X;
    public int Z;

    void OnEnable()
    {
        towerSpawner = FindAnyObjectByType<TowerSpawner>();
        GetTilePosition();
    }

    void Start(){
        gridManager = FindObjectOfType<GridManager>();
        SetIsWalkable();
    }

    void GetTilePosition()
    {
        X = Mathf.RoundToInt(transform.position.x);
        Z = Mathf.RoundToInt(transform.position.z);
    }

    void OnMouseDown()
    {
        if (isPlacable)
        {
            PlaceBallista();
        }
    }

    void PlaceBallista()
    {
        bool isPlaced = towerPrefab.InstantiateTower(towerPrefab, transform.position, towerSpawner);
        isPlacable = !isPlaced;
    }

    void SetIsWalkable(){
        if(gridManager == null){return;}

        Vector2Int tileCoordinates = gridManager.GetCoordinates(transform.position);
        Node currTile = gridManager.GetNode(tileCoordinates);
        
        if(currTile == null){return;}

        currTile.isWalkable = isPlacable;
    }
}
