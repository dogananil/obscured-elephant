using System.Collections.Generic;
using UnityEngine;

public class CardPool
{
    private List<CardView> _cardPool = new();
    private CardView _cardPrefab;
    private Transform _parent;

    public void Init(CardView prefab, int maxCount, Transform parent)
    {
        _cardPrefab = prefab;
        _parent = parent;

        for (int i = 0; i < maxCount; i++)
        {
            var card = GameObject.Instantiate(_cardPrefab, _parent).GetComponent<CardView>();
            card.gameObject.SetActive(false);
            _cardPool.Add(card);
        }
    }

    public CardView Get()
    {
        foreach (var card in _cardPool)
        {
            if (!card.gameObject.activeInHierarchy)
            {
                card.gameObject.SetActive(true);
                return card;
            }
        }

        Debug.LogWarning("[CardPool] No available card in pool!");
        return null;
    }

    public void ResetAll()
    {
        foreach (var card in _cardPool)
        {
            card.gameObject.SetActive(false);
            card.transform.SetParent(_parent, false); // Reset parent to pool
        }
    }
}
