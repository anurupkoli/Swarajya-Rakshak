using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 75;
    public bool InstantiateTower(Tower tower, Vector3 position, TowerSpawner towerSpawner)
    {
        Bank bank = FindAnyObjectByType<Bank>();
        if (bank == null) { return false; }

        if (bank.CurrentMoney >= towerCost)
        {
            GameObject ballista = Instantiate(tower.gameObject, position, Quaternion.identity);
            ballista.transform.parent = towerSpawner.gameObject.transform;
            bank.Retrive(towerCost);
            return true;
        }
        return false;
    }
}
