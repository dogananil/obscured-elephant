using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Handles card reveal, match checking, and selection logic.
/// </summary>
public class CardSelectionController : ISimpleItem
{
    private List<CardView> _selected = new();
    private bool _isLocked = false;

    public void Initialize()
    {
        
    }

    public void OnCardClicked(CardView card)
    {
        if (_isLocked || _selected.Contains(card) || card.IsRevealed)
            return;

        card.Reveal();
        _selected.Add(card);

        if (_selected.Count == 2)
        {
            HandleMatch().Forget();
        }
    }

    private async UniTaskVoid HandleMatch()
    {
        _isLocked = true;

        await UniTask.Delay(500);

        var a = _selected[0];
        var b = _selected[1];

        if (CardMatch.CardManager.AreMatching(a.Data, b.Data))
        {
            CardMatch.GameManager.RegisterMatch();
            a.Disable();
            b.Disable();
        }
        else
        {
            a.Hide();
            b.Hide();
        }
        CardMatch.GameManager.RegisterTurn();
        _selected.Clear();
        _isLocked = false;
    }
}
