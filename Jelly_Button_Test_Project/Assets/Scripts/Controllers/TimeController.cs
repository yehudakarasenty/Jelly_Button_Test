    using UnityEngine;
using UnityEngine.Events;

public class TimeController : ITimeController
{
    public float SecondsCounter { get; private set; }

    private readonly UnityEvent SecondPassedEvent = new UnityEvent();

    private int lastNotifyedSecond = 0;

    public TimeController()
    {
        SingleManager.Register<ITimeController>(this);
    }

    public void Init(){}

    public void StartGame()
    {
        SecondsCounter = 0;
        lastNotifyedSecond = 0;
    }

    public void Update()
    {
        SecondsCounter += Time.deltaTime;
        if (SecondsCounter > lastNotifyedSecond + 1)
        {
            lastNotifyedSecond++;
            SecondPassedEvent.Invoke();
        }
    }

    public void RegisterToSecondsNotifier(UnityAction action)
    {
        SecondPassedEvent.AddListener(action);
    }

    public void RemoveFromSecondsNotifier(UnityAction action)
    {
        SecondPassedEvent.RemoveListener(action);
    }

    public void Destroy()
    {
        SingleManager.Remove<ITimeController>();
    }
}
