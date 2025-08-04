using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Loads CardLibrary and handles level-related data such as card pool and grid configuration.
/// </summary>
public class LevelManager : IBootItem
{
    private const string LevelHolderAddress = "LevelHolder";

    private LevelHolder _levelHolder;
    public LevelHolder LevelHolder => _levelHolder;

    private LevelConfig _currentLevelConfig;
    public LevelConfig CurrentLevelConfig => _currentLevelConfig;

    public async UniTask BootAsync()
    {

        _levelHolder = await LoadLevelHolderAsync();
    }
    /// <summary>
    /// Load level holder from Addressables.
    /// </summary>
    public async UniTask<LevelHolder> LoadLevelHolderAsync()
    {
        LevelHolder holder = await CardMatch.Loader.LoadAssetAsync<LevelHolder>(LevelHolderAddress);
        if (holder == null)
        {
            Debug.LogError("[LevelManager] Failed to load LevelHolder");
        }
        return holder;
    }
    /// <summary>
    /// Get level config for a given level index. If none exists, it generates one.
    /// </summary>
    public LevelConfig GetLevelConfig(int index)
    {
        if (LevelHolder != null && index >= 0 && index < LevelHolder.levels.Count)
            return LevelHolder.levels[index];

        return GenerateDefaultLevelConfig(index);
    }
    public async UniTask GenerateLevel()
    {
        if (LevelHolder == null)
        {
            Debug.LogError("[LevelManager] LevelHolder is not loaded. Cannot generate level.");
            return;
        }
        // Get the current level index from SaveManager
        int currentLevelIndex = CardMatch.SaveManager.CurrentLevelIndex;
        // Get the level config for the current level
        _currentLevelConfig = GetLevelConfig(currentLevelIndex);
        // Load cards for this level
        await CardMatch.CardManager.LoadCardsForLevel(_currentLevelConfig);
        // Notify UI to update
        await CardMatch.UI.Hide("MenuView");
        await CardMatch.UI.Show("HUDView");
    }
    private LevelConfig GenerateDefaultLevelConfig(int level)
    {
        int pairCount = Mathf.Min(2 + level, 18); // max 18 pair = 36 cards
        int cardCount = pairCount * 2;

        Vector2Int gridSize = CalculateOptimalGrid(cardCount);

        var config = ScriptableObject.CreateInstance<LevelConfig>();
        config.rows = gridSize.y;
        config.columns = gridSize.x;
        return config;
    }

    /// <summary>
    /// Finds the most square-like grid that fits the given number of cards.
    /// </summary>
    private Vector2Int CalculateOptimalGrid(int cardCount)
    {
        int bestRows = 1;
        int bestCols = cardCount;
        int minDifference = int.MaxValue;

        for (int rows = 1; rows <= cardCount; rows++)
        {
            if (cardCount % rows != 0)
                continue;

            int cols = cardCount / rows;
            int diff = Mathf.Abs(rows - cols);

            if (diff < minDifference)
            {
                bestRows = rows;
                bestCols = cols;
                minDifference = diff;
            }
        }

        return new Vector2Int(bestCols, bestRows); // X = cols, Y = rows
    }
}
