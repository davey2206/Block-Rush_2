using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    bool isPlacable;
    public bool IsPlaceable { get { return isPlacable; } }

    [SerializeField]
    Tower TowerPrefab;

    GridManager gridManager;
    PathFinding PathFinding;
    Vector2Int Cor = new Vector2Int();

    private void Awake()
    {
        gridManager = FindAnyObjectByType<GridManager>();
        PathFinding = FindAnyObjectByType<PathFinding>();
    }
    private void Start()
    {
        if (gridManager != null)
        {
            Cor = gridManager.getCorFromPos(transform.position);

            if (!isPlacable)
            {
                gridManager.BlockNode(Cor);
            }
        }
    }
    private void OnMouseDown()
    {
        if (gridManager.GetNode(Cor).isWalkable && !PathFinding.WillBlockPath(Cor))
        {
            bool isPlaced = TowerPrefab.CreateTower(TowerPrefab, transform.position);

            if (isPlaced)
            {
                gridManager.BlockNode(Cor);
                PathFinding.NotifyReceivers();
            }
        }
    }
}