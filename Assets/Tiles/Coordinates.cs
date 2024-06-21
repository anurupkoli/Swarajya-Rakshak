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
        wayPoint = GetComponentInParent<WayPoint>();
        UpdateCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            UpdateCoordinates();
            UpdateObjectNames();
        }
        ToggleText();
    }

    void ToggleText(){
        if(Input.GetKeyDown(KeyCode.C)){
            enableText = !enableText;
        }
        text.enabled = enableText;
    }

    void UpdateCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.y);

        text.text = coordinates.x + ", " + coordinates.y;
        if(!wayPoint.IsPlacable){
            text.color = changeColor;
        }
    }

    void UpdateObjectNames()
    {
        transform.parent.name = text.text;
    }
}
