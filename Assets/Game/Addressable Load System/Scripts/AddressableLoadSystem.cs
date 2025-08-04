using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using System;

public class AddressableLoadSystem:ISimpleItem
{
    public void Initialize()
    {
        
    }

    public async UniTask<T> LoadAssetAsync<T>(string address) where T : class
    {
        try
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);
            await handle.ToUniTask();

            if (handle.Status == AsyncOperationStatus.Succeeded)
                return handle.Result;

            Debug.LogError($"[AddressableLoadSystem] Failed to load asset with address: {address}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[AddressableLoadSystem] Exception while loading asset: {e.Message}");
        }

        return null;
    }
}
