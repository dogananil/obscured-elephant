using Cysharp.Threading.Tasks;
using UnityEngine;

public static class CardMatch
{
    // Systems
    public static AddressableLoadSystem Loader = new();// Addressable Load System for loading assets

    public static async UniTask Boot()
    {
        CardLibrary library= await Loader.LoadAssetAsync<CardLibrary>("CardLibrary");
    }
}
