using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages loading and access to all CardData assets.
/// </summary>
public class CardManager : IBootItem
{
    private const string CardLibraryAddress = "CardLibrary";

    private List<CardData> _allCards = new();
    public IReadOnlyList<CardData> AllCards => _allCards;

    public async UniTask BootAsync()
    {
        var library = await CardMatch.Loader.LoadAssetAsync<CardLibrary>(CardLibraryAddress);

        if (library == null)
        {
            Debug.LogError("[CardManager] Failed to load CardLibrary.");
            return;
        }

        _allCards = library.Cards
            .Where(card => card != null)
            .ToList();
    }
    public List<CardData> GenerateRandomPairs(int pairCount)
    {
        var selected = _allCards
            .OrderBy(_ => Random.value)
            .Take(pairCount)
            .ToList();

        var fullList = selected
            .Concat(selected)
            .OrderBy(_ => Random.value)
            .ToList();

        return fullList;
    }

    public CardData GetById(int id)
    {
        return _allCards.FirstOrDefault(card => card.id == id);
    }

    public bool AreMatching(CardData a, CardData b)
    {
        return a != null && b != null && a.id == b.id;
    }
}
