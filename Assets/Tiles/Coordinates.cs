using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Coordinates : MonoBehaviour
{
    TextMeshPro text;
    Vector2Int coordinates = new Vector2Int();
    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        updateCoordinates();
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            updateCoordinates();
            updateObjectNames();
        }
    }

    void updateCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.y);

        text.text = coordinates.x + ", " + coordinates.y;
    }

    void updateObjectNames()
    {
        transform.parent.name = text.text;
    }
}
