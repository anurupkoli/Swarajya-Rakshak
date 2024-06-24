using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    List<Node> path = new List<Node>();
    PathFinder pathFinder;
    GridManager gridManager;
    Enemy enemy;

    void OnEnable()
    {
        FindObjectOfType<PathFinder>().RecalculatePathRequested += RecalculatePath;
        ReturnToStart();
        RecalculatePath(true);
    }

    void OnDisable()
    {
        FindObjectOfType<PathFinder>().RecalculatePathRequested -= RecalculatePath;
    }

    void Awake()
    {
        enemy = FindAnyObjectByType<Enemy>();
        pathFinder = FindAnyObjectByType<PathFinder>();
        gridManager = FindAnyObjectByType<GridManager>();
    }

    public void RecalculatePath()
    {
        RecalculatePath(false);
    }

    public void RecalculatePath(bool hasReset)
    {
        path.Clear();
        StopAllCoroutines();
        if (hasReset)
        {
            path = pathFinder.GetNewPath(pathFinder.StartCoords);
        }
        else
        {
            path = pathFinder.GetNewPath(gridManager.GetCoordinates(transform.position));
        }

        StartCoroutine(MoveEnemy());
    }

    void ReturnToStart()
    {
        gameObject.transform.position = gridManager.GetPosition(pathFinder.StartCoords);
    }

    IEnumerator MoveEnemy()
    {
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPosition(path[i].coordinates);

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

    void FinishMoving()
    {
        gameObject.SetActive(false);
        enemy.DeductMoney();
    }
}
