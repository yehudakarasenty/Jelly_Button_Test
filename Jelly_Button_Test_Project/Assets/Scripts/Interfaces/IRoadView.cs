using UnityEngine;

public interface IRoadView
{
    void AddPlane(Vector3 pos);
    void RemoveOldestPlane();
    float PlaneSize { get; }
    float PlanePositionY { get; }
}
