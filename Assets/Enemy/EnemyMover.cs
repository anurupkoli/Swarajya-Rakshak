using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    List<WayPoint> wayPoints = new List<WayPoint>();
    GameObject pathTiles;
    private void Start()
    {
        pathTiles = GameObject.FindGameObjectWithTag("PathTiles");
        FindPaths();
        ReturnToStart();
        StartCoroutine(MoveEnemy());
    }

    void FindPaths()
    {
        List<GameObject> paths = GameObject.FindGameObjectsWithTag("Path").ToList();
        paths = paths.OrderBy(path => path.name).ToList();
        AddToWayPoints(paths);
    }

    void AddToWayPoints(List<GameObject> paths){
        wayPoints.Clear();
        foreach (GameObject path in paths)
        {
            wayPoints.Add(path.GetComponent<WayPoint>());
            AddToPathTiles(path);
        }
    }

    void AddToPathTiles(GameObject path){
        path.transform.parent = pathTiles.transform;
    }

    void ReturnToStart(){
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
        Destroy(gameObject);
    }

    void HandleEnemyFacing(Vector3 endPosition){
         transform.LookAt(endPosition);
    }
}
