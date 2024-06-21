using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> wayPoints = new List<WayPoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    private void Start() {
        StartCoroutine(moveEnemy());
    }

    IEnumerator moveEnemy(){
        foreach(WayPoint wayPoint in wayPoints){
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(wayPoint.X, transform.position.y, wayPoint.Z);
            transform.LookAt(endPosition);
            float distance = 0f;
            while(distance < 1){
                distance += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, distance);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
