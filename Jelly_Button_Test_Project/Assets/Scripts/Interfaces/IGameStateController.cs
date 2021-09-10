using UnityEngine.Events;

public enum GameState {APP_INITED, PLAYING, GAME_OVER }
public interface IGameStateController: IController
{
    GameState GameState { get; }
    void RegisterToGameStateChange(UnityAction action);
    void RemoveFromGameStateChange(UnityAction action);
    void AppInited();
}
