
using UnityEngine.Events;

public interface IScoreController : IController
{
    int CurrentScore { get; }
    int HighestScore { get; }
    bool IsHighestScore { get; }
    void RegisterToScoreChangeNotifyer(UnityAction action);
    void RemoveFromScoreChangeNotifyer(UnityAction action);
    void RegisterToHighestScoreChangeNotifyer(UnityAction action);
    void RemoveFromHighestScoreChangeNotifyer(UnityAction action);
}
