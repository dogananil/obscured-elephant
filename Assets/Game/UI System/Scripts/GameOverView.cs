using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : View
{
    [SerializeField] private TextMeshProUGUI _summaryText;
    [SerializeField] private Button _nextLevelButton;

    protected override void Awake()
    {
        base.Awake();
        _nextLevelButton.onClick.AddListener(OnNextLevelClicked);
    }

    public void Setup(int turnCount)
    {
        _summaryText.text = $"You completed the level in {turnCount} turns!";
    }

    private void OnNextLevelClicked()
    {
        CardMatch.GameManager.StartGame().Forget();
        Hide().Forget();
    }
}
