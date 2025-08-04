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

        _cardPoolParent = new GameObject("CardPool");
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

        if (_allCards.Count == 0)
        {
            Debug.LogError("[CardManager] No cards available in library.");
            return;
        }

        // Select pairs (cards can repeat if needed)
        List<CardData> selectedPairs = new();
        while (selectedPairs.Count < pairCount)
        {
            var shuffled = _allCards.OrderBy(_ => Random.value).ToList();
            foreach (var card in shuffled)
            {
                selectedPairs.Add(card);
                if (selectedPairs.Count >= pairCount)
                    break;
            }
        }

        // Duplicate and shuffle
        var cardsToSpawn = selectedPairs
            .SelectMany(card => new[] { card, card })
            .OrderBy(_ => Random.value)
            .ToList();

        // Get card views
        List<CardView> views = new();
        foreach (var card in cardsToSpawn)
        {
            var view = _cardPool.Get();
            view.Init(card);
            views.Add(view);
        }

        // Get board view from UIManager
        BoardView boardView = CardMatch.UI.GetView<BoardView>() as BoardView;
        if (boardView == null)
        {
            Debug.LogError("[CardManager] BoardView not found from UIManager.");
            return;
        }

        var layoutSystem = new BoardLayoutSystem();
        layoutSystem.LayoutCards(views, new Vector2Int(config.columns,config.rows), boardView.CardContainer);

        await CardMatch.UI.Show("BoardView");
    }
}
