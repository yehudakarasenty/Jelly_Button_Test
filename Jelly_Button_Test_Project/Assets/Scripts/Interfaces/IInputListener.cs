using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInputListener
{
    void RegisterToHorizontalInput(UnityAction<float> action);
    void RemoveFromHorizontalInput(UnityAction<float> action);
}
