using Cysharp.Threading.Tasks;
using UnityEngine;

public static class CardMatch
{
    // Systems
    public static AddressableLoadSystem Loader = new();// Addressable Load System for loading assets
    public static SaveManager SaveManager = new(); // Save Manager for saving game state
    public static LevelManager LevelManager = new(); // Level Manager for handling levels and card data

    public static async UniTask Boot()
    {
        await SaveManager.BootAsync(); // Boot SaveManager
        await LevelManager.BootAsync(); // Boot LevelManager
    }
}
