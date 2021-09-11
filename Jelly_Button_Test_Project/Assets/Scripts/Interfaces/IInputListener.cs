using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsibility: Manage user Input and notify
/// </summary>
public interface IInputListener
{
    void RegisterToHorizontalInput(UnityAction<float> action);
    void RemoveFromHorizontalInput(UnityAction<float> action);
    bool IsKeyIsDown(KeyCode keyCode);
    bool AnyKey { get; }
}
