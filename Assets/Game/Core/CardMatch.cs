using Cysharp.Threading.Tasks;
using UnityEngine;

public static class CardMatch
{
    // Systems
    public static AddressableLoadSystem Loader = new();// Addressable Load System for loading assets
    public static SaveManager SaveManager = new(); // Save Manager for saving game state

    public static async UniTask Boot()
    {
        await SaveManager.BootAsync(); // Initialize SaveManager
        CardLibrary library= await Loader.LoadAssetAsync<CardLibrary>("CardLibrary");
    }
}
