using UnityEngine.Events;

public interface IObstaclesController : IController
{
    void SetView(IObstaclesView view);
    void RegisterToObstaclePassedNotifyer(UnityAction action);
    void RemoveFromObstaclePassedNotifyer(UnityAction action);
    int PassedObstacleAmount { get; }
}