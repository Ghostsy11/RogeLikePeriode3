using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPath : MonoBehaviour
{
    public GridManager gridManager;
    public PathFinder pathfinder;
    public Vector2Int start;
    public Vector2Int target;

    void OnDrawGizmos()
    {
        if (gridManager == null || pathfinder == null) return;

        List<Vector2Int> path = pathfinder.FindPath(start, target);
        Gizmos.color = Color.green;

        foreach (var point in path)
        {
            Vector3 worldPosition = new Vector3(point.x, point.y, 0);
            Gizmos.DrawCube(worldPosition, Vector3.one * 0.5f); // Draw path as small cubes
        }
    }
}
