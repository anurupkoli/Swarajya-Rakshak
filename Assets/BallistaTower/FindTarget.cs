using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        FindEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemy();
        transform.LookAt(enemy);
    }

    void FindEnemy(){
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }
}
