using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 5f)]
    float Speed = 1f;

    List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    PathFinding pathFinding;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinding = FindObjectOfType<PathFinding>();
    }

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int cor = new Vector2Int();

        if (resetPath)
        {
            cor = pathFinding.StartCor;
        }
        else
        {
            cor = gridManager.getCorFromPos(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinding.GetNewPath(cor);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPosFromCor(pathFinding.StartCor);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 StartPos = transform.position;
            Vector3 EndPos = gridManager.GetPosFromCor(path[i].Cor);
            float TrevelPrecent = 0f;

            transform.LookAt(EndPos);

            while (TrevelPrecent < 1f)
            {
                TrevelPrecent += Time.deltaTime * Speed;
                transform.position = Vector3.Lerp(StartPos, EndPos, TrevelPrecent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}
