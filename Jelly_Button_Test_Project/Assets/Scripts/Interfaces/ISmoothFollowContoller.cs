/// <summary>
/// Responsibility: Manage the camera movment class
/// </summary>
public interface ISmoothFollowController :IController
{
    void SetZoom(bool zoom);
    void SetView(ISmoothFollow view);
}
