using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Manages loading and access to all CardData assets.
/// </summary>
public class CardManager : IBootItem
{
    private const string CardLibraryAddress = "CardLibrary";

    private List<CardData> _allCards = new();
    public IReadOnlyList<CardData> AllCards => _allCards;

    private CardLibrary _cardLibrary;
    public CardLibrary CardLibrary => _cardLibrary;

    private CardPool _cardPool = new();
    private GameObject _cardPoolParent;

    public async UniTask BootAsync()
    {
        _cardLibrary = await CardMatch.Loader.LoadAssetAsync<CardLibrary>(CardLibraryAddress);

        if (_cardLibrary == null)
        {
            Debug.LogError("[CardManager] Failed to load CardLibrary.");
            return;
        }

        _allCards = _cardLibrary.Cards
            .Where(card => card != null)
            .ToList();
        _cardPoolParent= new GameObject("CardPool");
        _cardPool.Init(_cardLibrary.CardPrefab, _allCards.Count * 2, _cardPoolParent.transform);
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

    public async UniTask LoadCardsForLevel(LevelConfig config)
    {
        int totalCards = config.columns * config.rows;

        if (totalCards % 2 != 0)
        {
            Debug.LogError("[CardManager] Total card count must be even.");
            return;
        }

        int pairCount = totalCards / 2;

        if (AllCards.Count == 0)
        {
            Debug.LogError("[CardManager] No cards available in library.");
            return;
        }

        // Fill up enough pairs, even if it means reusing cards
        List<CardData> selectedPairs = new();

        while (selectedPairs.Count < pairCount)
        {
            var shuffled = AllCards.OrderBy(_ => UnityEngine.Random.value).ToList();
            foreach (var card in shuffled)
            {
                selectedPairs.Add(card);
                if (selectedPairs.Count >= pairCount)
                    break;
            }
        }

        // Duplicate each card to create matching pairs
        List<CardData> cardsToSpawn = new();
        foreach (var card in selectedPairs)
        {
            cardsToSpawn.Add(card);
            cardsToSpawn.Add(card);
        }

        // Shuffle
        cardsToSpawn = cardsToSpawn.OrderBy(_ => UnityEngine.Random.value).ToList();


        foreach (var card in cardsToSpawn)
        {
            // TODO: Instantiate prefab
        }

        await UniTask.Yield();
    }


}
