using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsibility: Hold and manage the GameState
/// </summary>
public class GameStateController : IGameStateController
{
    #region Memebers
    #region Dependencies
    private IPlayerController mPlayerController;
    private IInputListener mInputListener;
    #endregion

    public GameState GameState { get; private set; }
    private UnityEvent gameStateChangedEvent = new UnityEvent();
    #endregion

    #region Functions
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
        ChangeState(GameState.READY_TO_PLAY);
    }

    private void GameOver()
    {
        ChangeState(GameState.GAME_OVER);
    }

    public void PlayAgain()
    {
        if (GameState != GameState.GAME_OVER)
            Debug.LogError("Play again pressed but GameState != GameState.GAME_OVER");
        else
        {
            ChangeState(GameState.READY_TO_PLAY);
            SceneManager.LoadScene(0);
        }
    }

    private void ChangeState(GameState state)
    {
        GameState = state;
        gameStateChangedEvent.Invoke();
    }

    public void RegisterToGameStateChange(UnityAction action)=> gameStateChangedEvent.AddListener(action);

    public void RemoveFromGameStateChange(UnityAction action)=> gameStateChangedEvent.RemoveListener(action);
    
    public void Update()
    {
        if (GameState == GameState.READY_TO_PLAY && mInputListener.AnyKey)
            ChangeState(GameState.PLAYING);
    }

    public void Destroy()
    {
        SingleManager.Remove<IGameStateController>();
    }
    #endregion
}
