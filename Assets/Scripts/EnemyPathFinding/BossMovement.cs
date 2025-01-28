using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public GridManager gridManager; // Reference to the GridManager
    public Transform player; // Reference to the player's position
    public float speed = 2f; // Speed at which the boss moves
    private Queue<Vector2Int> path; // Queue storing the path for the boss to follow

    private PathFinder pathfinder; // Reference to the Pathfinder instance

    void Start()
    {
        pathfinder = new PathFinder(); // Initialize the Pathfinder
        pathfinder.GridSize = gridManager.gridSize; // Set the grid size for pathfinding

        StartCoroutine(UpdatePath()); // Start the coroutine to update the path
    }

    IEnumerator UpdatePath()
    {
        while (true)
        {
            // Get the boss's and player's positions as grid coordinates
            Vector2Int bossPosition = Vector2Int.RoundToInt(transform.position);
            Vector2Int playerPosition = Vector2Int.RoundToInt(player.position);

            // Find the path and store it in a queue
            path = new Queue<Vector2Int>(pathfinder.FindPath(bossPosition, playerPosition));

            // Wait for 0.5 seconds before recalculating the path
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        // Move along the path if there are nodes remaining
        if (path != null && path.Count > 0)
        {
            Vector2 targetPosition = path.Peek(); // Get the next position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // If the boss reaches the target position, remove it from the path
            if ((Vector2)transform.position == targetPosition)
            {
                path.Dequeue();
            }
        }
    }
}
