using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> wayPoints = new List<WayPoint>();
    [SerializeField] float enemyWaitTime = 1f;
    private void Start() {
        StartCoroutine(moveEnemy());
    }

    IEnumerator moveEnemy(){
        foreach(WayPoint wayPoint in wayPoints){
            transform.position = new Vector3(wayPoint.X, transform.position.y, wayPoint.Z);
            yield return new WaitForSeconds(enemyWaitTime);
        }
    }
}
