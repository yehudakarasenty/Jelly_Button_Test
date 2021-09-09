using UnityEngine;

public interface IRoadController : IController
{
    void StartGame();
    void SetView(IRoadView view);
    float RoadLength { get; }
    float RoadWidth { get; }
}
