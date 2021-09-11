using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsibility: Move the player and listen to collosions
/// </summary>
public interface IPlayerController: IController
{
    void SetView(IPlayerView playerView);
    void RegisterToOnBoostChange(UnityAction<bool> action);
    void RemoveFromOnBoostChange(UnityAction<bool> action);
    void RegisterToOnPlayerCollided(UnityAction action);
    void RemoveFromOnPlayerCollided(UnityAction action);
    Vector3 PlayerPosition { get; }
    Vector3 PlayerSize { get; }
    void PlyerCollided();
}
