using UnityEngine.Events;

/// <summary>
/// Responsibility: Create and manage obstacles
/// </summary>
public interface IObstaclesController : IController
{
    void SetView(IObstaclesView view);
    void RegisterToObstaclePassedNotifyer(UnityAction action);
    void RemoveFromObstaclePassedNotifyer(UnityAction action);
    int PassedObstacleAmount { get; }
}