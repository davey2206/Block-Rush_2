using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCor;
    public Vector2Int StartCor { get { return startCor; } }
    [SerializeField] Vector2Int endCor;
    public Vector2Int EndCor { get { return endCor; } }

    Node StartNode;
    Node EndNode;
    Node currentSearchNode;

    Queue<Node> Frontier = new Queue<Node>();

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> Reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left ,Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    private void Awake()
    {
        gridManager = FindAnyObjectByType<GridManager>();

        if (gridManager != null)
        {
            grid = gridManager.Grid;
            StartNode = grid[startCor];
            EndNode = grid[endCor];
        }
    }

    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCor);
    }

    public List<Node> GetNewPath(Vector2Int cor)
    {
        gridManager.ResetNode();
        BreadthFirstSearch(cor);
        return BuildPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors= new List<Node>();
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCor = currentSearchNode.Cor + direction;

            if (grid.ContainsKey(neighborCor))
            {
                neighbors.Add(grid[neighborCor]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!Reached.ContainsKey(neighbor.Cor) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                Reached.Add(neighbor.Cor, neighbor);
                Frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int cor)
    {
        StartNode.isWalkable = true;
        EndNode.isWalkable = true;

        Frontier.Clear();
        Reached.Clear();

        bool isRunning = true;

        Frontier.Enqueue(grid[cor]);
        Reached.Add(cor, grid[cor]);

        while (Frontier.Count > 0 && isRunning)
        {
            currentSearchNode = Frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode.Cor == endCor)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currantNode = EndNode;

        path.Add(currantNode);
        currantNode.isPath = true;

        while (currantNode.connectedTo != null)
        {
            currantNode = currantNode.connectedTo;
            path.Add(currantNode);
            currantNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int cor)
    {
        if (grid.ContainsKey(cor))
        {
            bool oldState = grid[cor].isWalkable;
            grid[cor].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[cor].isWalkable = oldState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
