using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerController: IController
{
    void StartGame();

    void EndGame();

    void SetView(IPlayerView playerView);
}
