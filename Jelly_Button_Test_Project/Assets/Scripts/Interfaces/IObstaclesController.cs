using UnityEngine.Events;

public interface IObstaclesController : IController
{
    void StartGame();
    void SetView(IObstaclesView view);
    void RegisterToObstaclePassedNotifyer(UnityAction action);
    void RemoveFromObstaclePassedNotifyer(UnityAction action);
    int PassedObstacleAmount { get; }
}