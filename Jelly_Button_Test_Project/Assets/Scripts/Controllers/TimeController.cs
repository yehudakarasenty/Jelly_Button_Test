using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsibility: Count the game time
/// </summary>
public class TimeController : ITimeController
{
    #region Members
    #region Dependencies
    private IGameStateController mGameStateController;
    #endregion
    public float SecondsCounter { get; private set; }

    private readonly UnityEvent SecondPassedEvent = new UnityEvent();

    private int lastNotifyedSecond = 0;
    #endregion

    #region Functions
    public TimeController()
    {
        SingleManager.Register<ITimeController>(this);
    }

    public void Init()
    {
        mGameStateController = SingleManager.Get<IGameStateController>();
        SecondsCounter = 0;
        lastNotifyedSecond = 0;
    }

    public void Update()
    {
        if (mGameStateController.GameState == GameState.PLAYING)
        {
            SecondsCounter += Time.deltaTime;
            if (SecondsCounter > lastNotifyedSecond + 1)
            {
                lastNotifyedSecond++;
                SecondPassedEvent.Invoke();
            }
        }
    }

    public void RegisterToSecondsNotifier(UnityAction action)=> SecondPassedEvent.AddListener(action);

    public void RemoveFromSecondsNotifier(UnityAction action)=>  SecondPassedEvent.RemoveListener(action);

    public void Destroy()
    {
        SingleManager.Remove<ITimeController>();
    }
    #endregion
}
