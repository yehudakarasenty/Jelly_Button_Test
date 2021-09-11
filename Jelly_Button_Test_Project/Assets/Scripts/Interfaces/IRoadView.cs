using UnityEngine;

/// <summary>
/// Responsibility: View of the road
/// </summary>
public interface IRoadView
{
    void AddPlane(Vector3 pos);
    void RemoveOldestPlane();
    float PlaneSize { get; }
    float PlanePositionY { get; }
}
