using UnityEngine;
using UnityEngine.Events;

public interface IPlayerController: IController
{
    void StartGame();
    void EndGame();
    void SetView(IPlayerView playerView);
    void RegisterToOnBoostChange(UnityAction<bool> action);
    void RemoveFromOnBoostChange(UnityAction<bool> action);
    Vector3 PlayerPosition { get; }
}
