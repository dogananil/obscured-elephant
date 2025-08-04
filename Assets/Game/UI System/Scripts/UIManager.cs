using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

public class UIManager : MonoBehaviour, IBootItem
{
    private const string ViewLibraryAddress = "ViewLibrary"; // Addressable adı
    private readonly Dictionary<string, View> views = new();

    private GameObject _uiRoot;
    public async UniTask BootAsync()
    {
        FindOrCreateUIRoot();

        var library = await CardMatch.Loader.LoadAssetAsync<ViewLibrary>(ViewLibraryAddress);
        if (library == null)
        {
            Debug.LogError("[UIManager] Failed to load ViewLibrary.");
            return;
        }

        foreach (var viewPrefab in library.Views)
        {
            if (viewPrefab == null)
            {
                Debug.LogWarning("[UIManager] Null view found in ViewLibrary.");
                continue;
            }

            View newView = Instantiate(viewPrefab, _uiRoot.transform);
            if (newView == null)
            {
                Debug.LogError($"[UIManager] View prefab {viewPrefab.name} is missing View component.");
                continue;
            }

            await newView.Hide();
            views[newView.ViewName] = newView;
        }
    }
    private void FindOrCreateUIRoot()
    {
        _uiRoot = GameObject.Find("UI");
        if (_uiRoot == null)
        {
            _uiRoot = new GameObject("UI");
        }
    }
    public async UniTask Show(string viewName)
    {
        if (views.TryGetValue(viewName, out var view))
        {
            await view.Show();
        }
        else
        {
            Debug.LogWarning($"[UIManager] View not found: {viewName}");
        }
    }

    public async UniTask Hide(string viewName)
    {
        if (views.TryGetValue(viewName, out var view))
        {
            await view.Hide();
        }
        else
        {
            Debug.LogWarning($"[UIManager] View not found: {viewName}");
        }
    }
}
