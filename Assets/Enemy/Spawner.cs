using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float timePerSpawn = 1f;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies(){
        while(true){
            Instantiate(prefab, gameObject.transform);
            yield return new WaitForSeconds(timePerSpawn);
        }
    }
}
