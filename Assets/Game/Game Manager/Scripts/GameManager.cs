using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : IBootItem
{
    private GameState _state;
    private int _turnCount;

    public async UniTask BootAsync()
    {
        _state = GameState.Menu;
        _turnCount = 0;

        await CardMatch.UI.Show("MenuView");
    }

    public async UniTask StartGame()
    {
        _state = GameState.Playing;
        _turnCount = 0;

        await CardMatch.LevelManager.GenerateLevel();
    }

    public async UniTask EndGame()
    {
        _state = GameState.GameOver;

        await CardMatch.UI.Hide("HUDView");
        await CardMatch.UI.Show("GameOverView");

        Debug.Log($"Game Over! Turns used: {_turnCount}");
    }

    public void IncrementTurn()
    {
        if (_state != GameState.Playing) return;
        _turnCount++;
    }

    public int GetTurnCount() => _turnCount;
    public GameState GetGameState() => _state;
}
