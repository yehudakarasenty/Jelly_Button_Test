using UnityEngine.Events;

public class GameStateController : IGameStateController
{
    private IPlayerController mPlayerController;
    private IInputListener mInputListener;
    public GameState GameState { get; private set; }
    private UnityEvent gameStateChangedEvent = new UnityEvent();

    public GameStateController()
    {
        SingleManager.Register<IGameStateController>(this);
    }

    public void Init()
    {
        mPlayerController = SingleManager.Get<IPlayerController>();
        mInputListener = SingleManager.Get<IInputListener>();
        mPlayerController.RegisterToOnPlayerCollided(GameOver);
    }

    public void AppInited()
    {
        ChangeState(GameState.APP_INITED);
    }

    private void GameOver()
    {
        ChangeState(GameState.GAME_OVER);
    }

    private void ChangeState(GameState state)
    {
        GameState = state;
        gameStateChangedEvent.Invoke();
    }

    public void RegisterToGameStateChange(UnityAction action)
    {
        gameStateChangedEvent.AddListener(action);
    }

    public void RemoveFromGameStateChange(UnityAction action)
    {
        gameStateChangedEvent.RemoveListener(action);
    }

    public void Update()
    {
        if (GameState == GameState.APP_INITED && mInputListener.AnyKey)
            ChangeState(GameState.PLAYING);
    }

    public void Destroy()
    {
        
    }
}
