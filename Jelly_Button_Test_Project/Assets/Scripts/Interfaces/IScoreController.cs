
using UnityEngine.Events;

public interface IScoreController : IController
{
    void StartGame();
    int CurrentScore { get; }
    int BestScore { get; }
    void EndGame();
    void RegisterToScoreChangeNotifyer(UnityAction action);
    void RemoveFromScoreChangeNotifyer(UnityAction action);
    void RegisterToBestScoreChangeNotifyer(UnityAction action);
    void RemoveFromBestScoreChangeNotifyer(UnityAction action);
}
