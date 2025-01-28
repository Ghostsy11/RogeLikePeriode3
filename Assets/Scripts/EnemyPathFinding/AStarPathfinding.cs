using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    // Define the grid's size (e.g., 10x10) in Unity Inspector
    public Vector2Int gridSize;

    // Starting position of the pathfinding
    public Vector2Int start;

    // Target position that the pathfinding aims to reach
    public Vector2Int target;

    // Offset distance to stop before reaching the exact target
    public float offset = 1.5f;
}
