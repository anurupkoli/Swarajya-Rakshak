using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] bool isPlacable = false;
    public bool IsPlacable{
        get{
            return isPlacable;
        }
    }

    [SerializeField] GameObject ballistaPrefab;
    GameObject ballistas;
    public int X;
    public int Z;

    void Start()
    {
        GetTilePosition();
        ballistas = GameObject.FindWithTag("Ballistas");
    }

    void GetTilePosition(){
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
        isPlacable = false;
        GameObject ballista = Instantiate(ballistaPrefab, transform.position, Quaternion.identity);
        ballista.transform.parent = ballistas.transform;
    }
}
