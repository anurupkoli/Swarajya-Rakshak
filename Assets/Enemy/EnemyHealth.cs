using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int health = 5;
    int particleHits = 0;
    Enemy enemy;

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }
    void OnEnable()
    {
        particleHits = 0;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void ProcessHit()
    {
        particleHits++;
        if (particleHits >= health)
        {
            enemy.RewardMoney();
            gameObject.SetActive(false);
        }
    }
}
