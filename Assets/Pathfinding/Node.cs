using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int Cor;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int Cor, bool isWalkable)
    {
        this.Cor = Cor;
        this.isWalkable = isWalkable;
    }
}
