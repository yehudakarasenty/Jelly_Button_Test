
using UnityEngine.Events;

/// <summary>
/// Responsibility: Count the score of the player (current score and highest score)
/// </summary>
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
