using System;
using UnityEngine.Events;

public interface ITimeController: IController
{
    float SecondsCounter { get; }

    void RegisterToSecondsNotifier(UnityAction action);

    void RemoveFromSecondsNotifier(UnityAction action);
}
