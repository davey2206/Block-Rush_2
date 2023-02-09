using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    Vector2Int GridSize;
    [Tooltip("Unity Grid Size - Should match unityEditor snap settings.")]
    [SerializeField]
    int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int cor)
    {
        if (grid.ContainsKey(cor))
        {
            return grid[cor];
        }
        return null;
    }

    public void BlockNode(Vector2Int cor)
    {
        if (grid.ContainsKey(cor))
        {
            grid[cor].isWalkable = false;
        }
    }

    public void ResetNode()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int getCorFromPos(Vector3 pos)
    {
        Vector2Int Cor = new Vector2Int();
        Cor.x = Mathf.RoundToInt(pos.x / unityGridSize);
        Cor.y = Mathf.RoundToInt(pos.z / unityGridSize);

        return Cor;
    }

    public Vector3 GetPosFromCor(Vector2Int cor)
    {
        Vector3 pos = new Vector3();
        pos.x = cor.x * unityGridSize;
        pos.z = cor.y * unityGridSize;

        return pos;
    }

    void CreateGrid()
    {
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                Vector2Int cor = new Vector2Int(x, y);
                grid.Add(cor, new Node(cor, true));
            }
        }
    }
}
