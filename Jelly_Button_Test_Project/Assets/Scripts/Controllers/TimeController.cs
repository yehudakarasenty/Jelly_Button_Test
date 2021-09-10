using UnityEngine;
using UnityEngine.Events;

public class TimeController : ITimeController
{
    private IGameStateController mGameStateController;
    public float SecondsCounter { get; private set; }

    private readonly UnityEvent SecondPassedEvent = new UnityEvent();

    private int lastNotifyedSecond = 0;

    public TimeController()
    {
        SingleManager.Register<ITimeController>(this);
    }

    public void Init()
    {
        mGameStateController = SingleManager.Get<IGameStateController>();
        mGameStateController.RegisterToGameStateChange(GameStateChange);
    }

    private void GameStateChange()
    {
        switch (mGameStateController.GameState)
        {
            case GameState.APP_INITED:
                SecondsCounter = 0;
                lastNotifyedSecond = 0;
                break;
            case GameState.PLAYING:
                break;
            case GameState.GAME_OVER:
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {

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
        mGameStateController.RemoveFromGameStateChange(GameStateChange);
        SingleManager.Remove<ITimeController>();
    }
}
