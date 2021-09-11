using UnityEngine;

/// <summary>
/// Responsibility: View of obstacles
/// </summary>
public interface IObstaclesView 
{
    float ObstaclePositionY { get; }
    void AddObstacle(Vector3 pos);
    void RemoveOldestObstacle();
}
