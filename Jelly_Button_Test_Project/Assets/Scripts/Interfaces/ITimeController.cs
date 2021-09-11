using UnityEngine.Events;

/// <summary>
/// Responsibility: Count the game time
/// </summary>
public interface ITimeController: IController
{
    float SecondsCounter { get; }
    void RegisterToSecondsNotifier(UnityAction action);
    void RemoveFromSecondsNotifier(UnityAction action);
}
