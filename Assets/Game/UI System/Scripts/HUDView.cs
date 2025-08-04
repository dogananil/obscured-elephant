using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : View
{
    [SerializeField]private TextMeshProUGUI _turnCountText;
    [SerializeField]private TextMeshProUGUI _matchesText;

    [SerializeField] private Button _menuButton;
    protected override void Awake()
    {
        base.Awake();
        _menuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    private void OnMenuButtonClicked()
    {
        
    }
    
    public void UpdateTurnCount(int turnCount)
    {
        _turnCountText.text = $"Turns: {turnCount}";
    }
    public void UpdateMatches(int matches)
    {
        _matchesText.text = $"Matches: {matches}";
    }
}
