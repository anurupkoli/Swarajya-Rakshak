using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    [SerializeField] ParticleSystem boultParticles;
    [SerializeField] float range = 15f;
    Transform target;

    void Update()
    {
        FindClosestTarget();
        LookAtTarget();
    }

    void FindClosestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies == null){return;}

        GameObject closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy;
                maxDistance = targetDistance;
            }
        }

        if(closestTarget == null){return;}
        target = closestTarget.transform;
    }

    void LookAtTarget()
    {
        if(target == null){return;}
        float distance = Vector3.Distance(transform.position, target.transform.position);
        transform.GetChild(1).LookAt(target);
        if (distance <= range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isAttacking)
    {
        var emissionModule = boultParticles.emission;
        emissionModule.enabled = isAttacking;
    }
}
