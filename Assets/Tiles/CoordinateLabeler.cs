using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    Color DefaultColor = Color.gray;
    [SerializeField]
    Color BlockedColor = Color.red;
    [SerializeField]
    Color ExporedColor = Color.yellow;
    [SerializeField]
    Color PathColor = Color.green;

    TextMeshPro label;
    Vector2Int Cor = new Vector2Int();
    GridManager gridManager;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();

        label.enabled = false;
        if (!Application.isPlaying)
        {
            label.enabled = true;
        }
        DisplayCor();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCor();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(Cor);

        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = BlockedColor;
        }
        else if (node.isPath)
        {
            label.color = PathColor;
        }
        else if (node.isExplored)
        {
            label.color = ExporedColor;
        }
        else
        {
            label.color = DefaultColor;
        }
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCor()
    {
        if (gridManager == null)
        {
            return;
        }
        Cor.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        Cor.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = Cor.x + "," + Cor.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = Cor.ToString();
    }
}
