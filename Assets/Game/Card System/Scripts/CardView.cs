using System;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    private CardData _data;
    private bool _isRevealed;
    public bool IsRevealed => _isRevealed;
    public CardData Data => _data;

    public void Init(CardData data)
    {
        _data = data;

        HideInstant();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        CardMatch.Selection.OnCardClicked(this);
    }

    public void Reveal()
    {
        _isRevealed = true;
        _icon.enabled = true;
        _icon.sprite = _data.icon;
    }

    public void Hide()
    {
        _isRevealed = false;
        _icon.enabled = true;
        _icon.sprite = CardMatch.CardManager.CardLibrary.BackIconOfCard;
    }

    public void HideInstant()
    {
        _isRevealed = false;
        _icon.enabled = true;
        _icon.sprite = CardMatch.CardManager.CardLibrary.BackIconOfCard;
    }

    public void Disable()
    {
        _button.interactable = false;
        _icon.enabled = false;
    }

    internal void Reset()
    {
        _isRevealed = false;
        _icon.enabled = true;
        _icon.sprite = CardMatch.CardManager.CardLibrary.BackIconOfCard;
        _button.interactable = true;
    }
}
