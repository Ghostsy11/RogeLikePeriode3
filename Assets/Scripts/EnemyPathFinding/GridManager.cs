using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10); // Size of the grid
    public GameObject tilePrefab; // Reference to the Tile prefab
    public float tileSpacing = 1f; // Spacing between tiles

    private GameObject[,] gridTiles; // Stores references to all spawned tiles

    void Start()
    {
        // Initialize and spawn the grid
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        // Create a 2D array to store the tiles
        gridTiles = new GameObject[gridSize.x, gridSize.y];

        // Loop through each grid position
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                // Calculate world position for the tile
                Vector3 worldPosition = new Vector3(x * tileSpacing, y * tileSpacing, 0);

                // Instantiate the tile prefab at the calculated position
                GameObject spawnedTile = Instantiate(tilePrefab, worldPosition, Quaternion.identity, transform);

                // Set the tile's name for clarity
                spawnedTile.name = $"Tile ({x}, {y})";

                // Store the tile in the grid array
                gridTiles[x, y] = spawnedTile;
            }
        }
    }

    // Example function to update tile costs
    public void UpdateTileCosts(Vector2Int position, int fCost, int gCost, int hCost)
    {
        // Ensure the position is within bounds
        if (position.x >= 0 && position.x < gridSize.x && position.y >= 0 && position.y < gridSize.y)
        {
            // Get the tile at the specified position
            GameObject tile = gridTiles[position.x, position.y];
            Tile tileScript = tile.GetComponent<Tile>();

            // Update the costs displayed on the tile
            if (tileScript != null)
            {
                tileScript.SetCosts(fCost, gCost, hCost);
            }
        }
    }
}