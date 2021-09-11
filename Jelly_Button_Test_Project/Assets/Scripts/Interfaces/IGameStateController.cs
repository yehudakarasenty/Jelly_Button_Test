using UnityEngine.Events;

public enum GameState {READY_TO_PLAY, PLAYING, GAME_OVER }

/// <summary>
/// Responsibility: Hold and manage the GameState
/// </summary>
public interface IGameStateController: IController
{
    GameState GameState { get; }
    void RegisterToGameStateChange(UnityAction action);
    void RemoveFromGameStateChange(UnityAction action);
    void AppInited();
    void PlayAgain();
}
