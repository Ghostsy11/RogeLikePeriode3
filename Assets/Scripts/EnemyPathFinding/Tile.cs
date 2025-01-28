using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TextMeshPro fCostText; // Reference to the text displaying fCost
    public TextMeshPro gCostText; // Reference to the text displaying gCost
    public TextMeshPro hCostText; // Reference to the text displaying hCost

    public void SetCosts(int fCost, int gCost, int hCost)
    {
        // Update the text to show the costs
        fCostText.text = $"F: {fCost}";
        gCostText.text = $"G: {gCost}";
        hCostText.text = $"H: {hCost}";
    }
}
