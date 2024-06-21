using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float timePerSpawn = 1f;
    [SerializeField] int poolSize = 5;
    GameObject[] pool;

    void Awake()
    {
        pool = new GameObject[poolSize];
        PopulatePool();
    }
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(prefab, gameObject.transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectsInPool()
    {
        foreach (GameObject prefab in pool)
        {
            if (!prefab.activeInHierarchy)
            {
                prefab.SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnableObjectsInPool();
            yield return new WaitForSeconds(timePerSpawn);
        }
    }
}
