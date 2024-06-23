using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Coordinates : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color changeColor = Color.gray;
    [SerializeField] Color pathColor = Color.red;
    [SerializeField] Color exploredColor = Color.yellow;
    TextMeshPro text;
    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();
    bool enableText = true;
    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        UpdateCoordinates();
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
        if(gridManager == null){return;}

        Node node = gridManager.GetNode(coordinates);
        if(node == null){return;}

        if(!node.isWalkable){
            text.color = changeColor;
        }
        else if(node.isPath){
            text.color = pathColor;
        }
        else if(node.isExplored){
            text.color = exploredColor;
        }
        else{
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
