using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Coordinates : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color changeColor = Color.gray;
    TextMeshPro text;
    Vector2Int coordinates = new Vector2Int();
    WayPoint wayPoint;
    bool enableText = true;
    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        UpdateCoordinates();
        wayPoint = GetComponentInParent<WayPoint>();
    }

    void Start()
    {
        enableText = false;
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            text.enabled = true;
            UpdateCoordinates();
            UpdateObjectNames();
        }
        ToggleText();
        ChangeTextColor();
    }

    void ToggleText()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            enableText = !enableText;
        }
        text.enabled = enableText;
    }

    void ChangeTextColor()
    {

        if (!wayPoint.IsPlacable)
        {
            text.color = changeColor;
        }
        else
        {
            text.color = defaultColor;
        }
    }

    void UpdateCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.y);

        text.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectNames()
    {
        transform.parent.name = text.text;
    }
}
