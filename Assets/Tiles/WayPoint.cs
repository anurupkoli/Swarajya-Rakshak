using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] bool isPlacable = false;
    [SerializeField] Tower towerPrefab;
    public bool IsPlacable
    {
        get
        {
            return isPlacable;
        }
    }

    [SerializeField] GameObject ballistaPrefab;
    TowerSpawner towerSpawner;
    public int X;
    public int Z;

    void OnEnable()
    {
        towerSpawner = FindAnyObjectByType<TowerSpawner>();
        GetTilePosition();
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
}
