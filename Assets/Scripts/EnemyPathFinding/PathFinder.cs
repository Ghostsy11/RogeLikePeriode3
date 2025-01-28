using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public GridManager gridManager; // Reference to the GridManager, used to update tile costs visually
    public Vector2Int GridSize; // The size of the grid
    public float Offset = 1.5f; // Offset to define how close the boss needs to be to the player

    // Finds the path from the start position to the target position using the A* algorithm
    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
    {
        // List of nodes to be evaluated (open list)
        List<Vector2Int> openList = new List<Vector2Int>();

        // Set of nodes already evaluated (closed list)
        HashSet<Vector2Int> closedList = new HashSet<Vector2Int>();

        // Maps each node to its parent node, used to reconstruct the path
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        // Stores the movement cost from the start node to each node
        Dictionary<Vector2Int, int> gScore = new Dictionary<Vector2Int, int>();

        // Stores the estimated total cost (gScore + heuristic) for each node
        Dictionary<Vector2Int, int> fScore = new Dictionary<Vector2Int, int>();

        // Add the start position to the open list and initialize its scores
        openList.Add(start);
        gScore[start] = 0; // The cost to reach the start node is 0
        fScore[start] = Heuristic(start, target); // Estimated cost to the target

        // Main A* algorithm loop
        while (openList.Count > 0)
        {
            // Get the node in the open list with the lowest fScore
            Vector2Int current = GetLowestFScoreNode(openList, fScore);

            // If the current node is close enough to the target, reconstruct and return the path
            if (Vector2.Distance(current, target) <= Offset)
            {
                return ReconstructPath(current, cameFrom);
            }

            // Remove the current node from the open list and add it to the closed list
            openList.Remove(current);
            closedList.Add(current);

            // Check all neighboring nodes
            foreach (var neighbor in GetNeighbors(current))
            {
                // If the neighbor is in the closed list, skip it
                if (closedList.Contains(neighbor)) continue;

                // Calculate the tentative gScore for this neighbor
                int tentativeGScore = gScore[current] + 1; // Assuming uniform cost for movement

                // If the neighbor is not in the open list, add it
                if (!openList.Contains(neighbor))
                    openList.Add(neighbor);
                else if (tentativeGScore >= gScore[neighbor])
                    // If this path is not better than a previously found path, skip it
                    continue;

                // Update the path and scores for this neighbor
                cameFrom[neighbor] = current; // Record the parent node for path reconstruction
                gScore[neighbor] = tentativeGScore; // Update gScore
                fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, target); // Update fScore

                // Update tile costs in the grid for debugging or visualization
                gridManager.UpdateTileCosts(neighbor, fScore[neighbor], gScore[neighbor], Heuristic(neighbor, target));
            }
        }

        // If the open list is empty and the target was not reached, log a failure
        Debug.Log("No Path Found");
        return new List<Vector2Int>(); // Return an empty path
    }

    // Get the node with the lowest fScore in the open list
    private Vector2Int GetLowestFScoreNode(List<Vector2Int> openList, Dictionary<Vector2Int, int> fScore)
    {
        Vector2Int lowest = openList[0]; // Start with the first node

        // Compare all nodes in the open list
        foreach (var node in openList)
        {
            if (fScore[node] < fScore[lowest]) // Update if the current node has a lower fScore
                lowest = node;
        }
        return lowest; // Return the node with the lowest fScore
    }

    // Get all valid neighbors of a node
    private List<Vector2Int> GetNeighbors(Vector2Int node)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        // Define movement directions: up, right, down, left
        Vector2Int[] directions = {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0)
        };

        // Add each valid neighbor
        foreach (var dir in directions)
        {
            Vector2Int neighbor = node + dir;
            if (IsWithinGrid(neighbor)) // Check if within grid bounds
                neighbors.Add(neighbor);
        }

        return neighbors; // Return the list of neighbors
    }

    // Check if a position is within the grid bounds
    private bool IsWithinGrid(Vector2Int position)
    {
        return position.x >= 0 && position.x < GridSize.x && position.y >= 0 && position.y < GridSize.y;
    }

    // Calculate the heuristic (Manhattan Distance)
    private int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    // Reconstruct the path from the target to the start
    private List<Vector2Int> ReconstructPath(Vector2Int current, Dictionary<Vector2Int, Vector2Int> cameFrom)
    {
        List<Vector2Int> path = new List<Vector2Int> { current }; // Start with the target
        while (cameFrom.ContainsKey(current)) // Follow the parent map back to the start
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse(); // Reverse the path to go from start to target
        return path; // Return the path
    }
}
