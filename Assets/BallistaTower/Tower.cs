using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 75;
    [SerializeField] float buildTime = 1f;

    void Start()
    {
        StartCoroutine(Build());
    }
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

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);

            yield return new WaitForSeconds(buildTime);

            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }
}
