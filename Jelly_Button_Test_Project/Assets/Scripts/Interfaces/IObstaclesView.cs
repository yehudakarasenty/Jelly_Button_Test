using UnityEngine;

public interface IObstaclesView 
{
    float ObstaclePositionY { get; }
    void AddObstacle(Vector3 pos);
    void RemoveOldestObstacle();
}
