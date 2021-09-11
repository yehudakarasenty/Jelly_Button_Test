/// <summary>
/// Responsibility: Manage the UI screen (labels, buttons, etc)
/// </summary>
public interface IUiController : IController
{
    void SetView(IUiView view);
    void PlayAgainButtonClicked();
}
