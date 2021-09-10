
using UnityEngine.Events;

public interface IScoreController : IController
{
    int CurrentScore { get; }
    int BestScore { get; }
    void RegisterToScoreChangeNotifyer(UnityAction action);
    void RemoveFromScoreChangeNotifyer(UnityAction action);
    void RegisterToBestScoreChangeNotifyer(UnityAction action);
    void RemoveFromBestScoreChangeNotifyer(UnityAction action);
}
