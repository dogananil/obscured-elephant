using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dynamically arranges card views in a grid layout based on level config.
/// </summary>
public class BoardLayoutSystem
{
    public void LayoutCards(List<CardView> cardViews, Vector2Int gridSize, RectTransform container)
    {
        if (container == null)
        {
            Debug.LogError("[BoardLayoutSystem] Container is null.");
            return;
        }

        var grid = container.GetComponent<GridLayoutGroup>();
        if (grid == null)
        {
            Debug.LogError("[BoardLayoutSystem] GridLayoutGroup is missing on container.");
            return;
        }

        // Calculate available width/height (exclude spacing)
        float spacing = 10f; // constant spacing
        float totalSpacingX = (gridSize.x - 1) * spacing;
        float totalSpacingY = (gridSize.y - 1) * spacing;

        float availableWidth = container.rect.width - totalSpacingX;
        float availableHeight = container.rect.height - totalSpacingY;

        float cellWidth = availableWidth / gridSize.x;
        float cellHeight = availableHeight / gridSize.y;

        Vector2 cellSize = new Vector2(cellWidth, cellHeight);

        grid.spacing = new Vector2(spacing, spacing);
        grid.cellSize = cellSize;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = gridSize.x;

        // Clear any existing children
        CardMatch.CardManager.ResetCardPool();

        // Add cards to container
        foreach (var card in cardViews)
        {
            card.transform.SetParent(container, false);
            card.gameObject.SetActive(true);
        }
    }
}
