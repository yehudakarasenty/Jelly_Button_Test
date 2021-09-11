/// <summary>
/// Responsibility: Build the road
/// </summary>
public interface IRoadController : IController
{
    void SetView(IRoadView view);
    float RoadLength { get; }
    float RoadWidth { get; }
}
