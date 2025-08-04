using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : IBootItem
{
    private GameState _state;
    private int _turnCount;
    private int _matchCount;
    private int _totalPairs=> (CardMatch.LevelManager.CurrentLevelConfig.rows*CardMatch.LevelManager.CurrentLevelConfig.columns)/2;

    public async UniTask BootAsync()
    {
        _state = GameState.Menu;
        _turnCount = 0;
        _matchCount = 0;

        await CardMatch.UI.Show("MenuView");
    }

    public async UniTask StartGame()
    {
        _state = GameState.Playing;
        _turnCount = 0;
        _matchCount = 0;

        HUDView hudView = CardMatch.UI.GetView<HUDView>() as HUDView;
        hudView.Setup();

        await CardMatch.LevelManager.GenerateLevel();
    }

    public void RegisterTurn()
    {
        if (_state != GameState.Playing) return;

        _turnCount++;
        HUDView hudView = CardMatch.UI.GetView<HUDView>() as HUDView;
        hudView.UpdateTurnCount(_turnCount);
    }

    public async UniTask RegisterMatch()
    {
        if (_state != GameState.Playing) return;

        _matchCount++;

        HUDView hudView = CardMatch.UI.GetView<HUDView>() as HUDView;
        hudView.UpdateMatches(_matchCount);

        if (_matchCount >= _totalPairs)
        {
            await EndGame();
        }
    }

    public async UniTask EndGame()
    {
        _state = GameState.GameOver;

        await CardMatch.UI.Hide("HUDView");
        await CardMatch.UI.Show("GameOverView");

        Debug.Log($"Game Over! Turns used: {_turnCount}");

        // Save next level index
        CardMatch.SaveManager.SaveLevelIndex(CardMatch.SaveManager.CurrentLevelIndex + 1);
    }

    public int GetTurnCount() => _turnCount;
    public GameState GetGameState() => _state;
}
