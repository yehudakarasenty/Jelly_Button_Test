using UnityEngine;
using UnityEngine.Events;

public interface IInputListener
{
    void RegisterToHorizontalInput(UnityAction<float> action);
    void RemoveFromHorizontalInput(UnityAction<float> action);
    bool IsKeyIsDown(KeyCode keyCode);
    bool AnyKey { get; }
}
