using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsibility: Count the score of the player (current score and highest score)
/// </summary>
public class ScoreController : IScoreController
{
    #region Members
    #region Const
    private const string HIGHEST_SCORE_KEY = "highest_score";
    #endregion

    #region Dependencies
    private IObstaclesController mObstaclesController;
    private ITimeController mTimeController;
    private IPlayerController mPlayerController;
    private IGameStateController mGameStateController;
    #endregion

    private readonly UnityEvent scoreChangeEvent = new UnityEvent();

    private readonly UnityEvent highestScoreChangeEvent = new UnityEvent();

    public int CurrentScore { get; private set; }

    public int HighestScore { get; private set; }

    public bool IsHighestScore { get; private set; } = false;

    private bool boost = false;
    #endregion

    #region Functions
    public ScoreController()
    {
        SingleManager.Register<IScoreController>(this);
    }

    public void Init()
    {
        mObstaclesController = SingleManager.Get<IObstaclesController>();
        mTimeController = SingleManager.Get<ITimeController>();
        mPlayerController = SingleManager.Get<IPlayerController>();
        mGameStateController = SingleManager.Get<IGameStateController>();

        mObstaclesController.RegisterToObstaclePassedNotifyer(ObstaclePassed);
        mTimeController.RegisterToSecondsNotifier(SecondPassed);
        mPlayerController.RegisterToOnBoostChange(BoostChanged);
        mGameStateController.RegisterToGameStateChange(GameStateChange);

        HighestScore = PlayerPrefs.GetInt(HIGHEST_SCORE_KEY, 0);
        CurrentScore = 0;
    }

    private void GameStateChange()
    {
        if (mGameStateController.GameState == GameState.GAME_OVER)
            if (HighestScore > PlayerPrefs.GetInt(HIGHEST_SCORE_KEY, 0))
                PlayerPrefs.SetInt(HIGHEST_SCORE_KEY, HighestScore);
    }

    private void BoostChanged(bool boost) => this.boost = boost;

    private void SecondPassed()=> AddToScore(boost ? 2 : 1);

    private void ObstaclePassed()=>  AddToScore(5);

    private void AddToScore(int scoreToAdd)
    {
        CurrentScore += scoreToAdd;
        scoreChangeEvent.Invoke();
        if (CurrentScore > HighestScore)
        {
            IsHighestScore = true;
            HighestScore = CurrentScore;
            highestScoreChangeEvent.Invoke();
        }
    }

    public void RegisterToScoreChangeNotifyer(UnityAction action)=> scoreChangeEvent.AddListener(action);

    public void RemoveFromScoreChangeNotifyer(UnityAction action)=> scoreChangeEvent.RemoveListener(action);

    public void RegisterToHighestScoreChangeNotifyer(UnityAction action)=> highestScoreChangeEvent.AddListener(action);

    public void RemoveFromHighestScoreChangeNotifyer(UnityAction action) => highestScoreChangeEvent.RemoveListener(action);

    public void Update() {}

    public void Destroy()
    {
        SingleManager.Remove<IScoreController>();
        mObstaclesController.RemoveFromObstaclePassedNotifyer(ObstaclePassed);
        mTimeController.RemoveFromSecondsNotifier(SecondPassed);
        mPlayerController.RemoveFromOnBoostChange(BoostChanged);
        mGameStateController.RemoveFromGameStateChange(GameStateChange);
    }
    #endregion
}
