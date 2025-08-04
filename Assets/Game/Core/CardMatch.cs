using Cysharp.Threading.Tasks;

public static class CardMatch
{
    // Systems
    public static AddressableLoadSystem Loader = new();// Addressable Load System for loading assets
    public static SaveManager SaveManager = new(); // Save Manager for saving game state
    public static LevelManager LevelManager = new(); // Level Manager for handling levels
    public static CardManager CardManager = new(); // Card Manager for managing card data and matching logic
    public static UIManager UI = new(); // UI Manager for handling user interface

    public static async UniTask Boot()
    {
        await UI.BootAsync(); // Boot UIManager
        await SaveManager.BootAsync(); // Boot SaveManager
        await CardManager.BootAsync(); // Boot CardManager
        await LevelManager.BootAsync(); // Boot LevelManager
    }
}
