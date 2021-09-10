public interface IObstaclesController : IController
{
    void StartGame();

    void SetView(IObstaclesView view);
}