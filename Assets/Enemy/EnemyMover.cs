using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    List<WayPoint> wayPoints = new List<WayPoint>();
    GameObject pathTiles;
    Enemy enemy;
    void OnEnable()
    {
        pathTiles = GameObject.FindGameObjectWithTag("PathTiles");
        FindPaths();
        ReturnToStart();
        StartCoroutine(MoveEnemy());
    }

    void Start()
    {
        enemy = FindAnyObjectByType<Enemy>();
    }

    void FindPaths()
    {
        wayPoints.Clear();
        foreach(Transform child in pathTiles.transform){
            wayPoints.Add(child.GetComponent<WayPoint>());
        }
    }

    void ReturnToStart()
    {
        gameObject.transform.position = wayPoints[0].transform.position;
    }

    IEnumerator MoveEnemy()
    {
        foreach (WayPoint wayPoint in wayPoints)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(wayPoint.X, transform.position.y, wayPoint.Z);

            HandleEnemyFacing(endPosition);

            float distance = 0f;
            while (distance < 1)
            {
                distance += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, distance);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishMoving();
    }

    void HandleEnemyFacing(Vector3 endPosition)
    {
        transform.LookAt(endPosition);
    }

    void FinishMoving(){
        gameObject.SetActive(false);
        enemy.DeductMoney();
    }
}
