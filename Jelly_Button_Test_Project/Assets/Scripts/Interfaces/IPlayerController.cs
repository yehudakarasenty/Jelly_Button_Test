using UnityEngine;

public interface IPlayerController: IController
{
    void StartGame();

    void EndGame();

    void SetView(IPlayerView playerView);

    Vector3 PlayerPosition { get; }
}
