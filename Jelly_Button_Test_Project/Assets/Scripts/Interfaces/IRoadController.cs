using UnityEngine;

public interface IRoadController : IController
{
    void SetView(IRoadView view);
    float RoadLength { get; }
    float RoadWidth { get; }
}
