using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    [SerializeField] int difficultyRamp = 1;
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
            health += difficultyRamp;
            gameObject.SetActive(false);
        }
    }
}
