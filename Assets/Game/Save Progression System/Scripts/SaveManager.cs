using Cysharp.Threading.Tasks;
using UnityEngine;

public class SaveManager : IBootItem
{
    private const string LevelKey = "currentLevelIndex";
    public int CurrentLevelIndex { get; private set; }

    public async UniTask BootAsync()
    {
        // Simulate async load (from PlayerPrefs or a file later)
        await UniTask.Yield();
        CurrentLevelIndex = PlayerPrefs.GetInt(LevelKey, 0);
    }

    public void SaveLevelIndex(int index)
    {
        CurrentLevelIndex = index;
        PlayerPrefs.SetInt(LevelKey, index);
        PlayerPrefs.Save();
    }
}
