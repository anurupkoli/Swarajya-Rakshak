using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] GameObject ballistaPrefab;
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlacable = false;
    public bool IsPlacable { get { return isPlacable; } }
    GridManager gridManager;
    PathFinder pathFinder;

    TowerSpawner towerSpawner;
    public int X;
    public int Z;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindAnyObjectByType<PathFinder>();
    }

    void OnEnable()
    {
        towerSpawner = FindAnyObjectByType<TowerSpawner>();
        GetTilePosition();
    }

    void Start()
    {
        SetIsWalkable();
    }

    void GetTilePosition()
    {
        X = Mathf.RoundToInt(transform.position.x);
        Z = Mathf.RoundToInt(transform.position.z);
    }

    void OnMouseDown()
    {
        if (gridManager == null) { return; }

        Vector2Int coordinates = gridManager.GetCoordinates(transform.position);
        if (coordinates == null) { return; }

        Node currNode = gridManager.GetNode(coordinates);
        if (currNode.isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            PlaceBallista();
        }
    }

    void PlaceBallista()
    {
        bool isPlaced = towerPrefab.InstantiateTower(towerPrefab, transform.position, towerSpawner);
        isPlacable = !isPlaced;
        if (isPlaced)
        {
            gridManager.BlockNode(gridManager.GetCoordinates(transform.position));
            pathFinder.RequestRecalculatePath();

        }
    }

    void SetIsWalkable()
    {
        if (gridManager == null) { return; }

        Vector2Int tileCoordinates = gridManager.GetCoordinates(transform.position);

        if (isPlacable == false)
        {
            gridManager.BlockNode(tileCoordinates);
        }
    }
}
