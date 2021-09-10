public interface IUiController : IController
{
    void SetView(IUiView view);

    void PlayAgainButtonClicked();
}
