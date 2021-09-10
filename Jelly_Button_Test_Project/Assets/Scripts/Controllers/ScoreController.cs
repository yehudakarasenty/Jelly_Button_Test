using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : IScoreController
{
    private IObstaclesController mObstaclesController;
    private ITimeController mTimeController;
    private IPlayerController mPlayerController;
    private IGameStateController mGameStateController;

    private readonly UnityEvent scoreChangeEvent = new UnityEvent();
    private readonly UnityEvent bestScoreChangeEvent = new UnityEvent();

    public int CurrentScore { get; private set; }

    public int BestScore { get; private set; }//TODO

    private bool boost = false;

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

        BestScore = PlayerPrefs.GetInt("best_score", 0);
    }

    private void GameStateChange()
    {
        switch (mGameStateController.GameState)
        {
            case GameState.APP_INITED:
                CurrentScore = 0;
                break;
            case GameState.PLAYING:
                break;
            case GameState.GAME_OVER:
                if (BestScore > PlayerPrefs.GetInt("best_score", 0))
                    PlayerPrefs.SetInt("best_score", BestScore);
                break;
            default:
                break;
        }
    }

    private void BoostChanged(bool boost) => this.boost = boost;

    private void SecondPassed()=> AddToScore(boost ? 2 : 1);

    private void ObstaclePassed()=>  AddToScore(5);

    private void AddToScore(int scoreToAdd)
    {
        CurrentScore += scoreToAdd;
        scoreChangeEvent.Invoke();
        if (CurrentScore > BestScore)
        {
            BestScore = CurrentScore;
            bestScoreChangeEvent.Invoke();
        }
    }

    public void RegisterToScoreChangeNotifyer(UnityAction action)=> scoreChangeEvent.AddListener(action);

    public void RemoveFromScoreChangeNotifyer(UnityAction action)=> scoreChangeEvent.RemoveListener(action);

    public void RegisterToBestScoreChangeNotifyer(UnityAction action)=> bestScoreChangeEvent.AddListener(action);

    public void RemoveFromBestScoreChangeNotifyer(UnityAction action) => bestScoreChangeEvent.RemoveListener(action);

    public void Update() {}

    public void Destroy()
    {
        SingleManager.Remove<IScoreController>();
        mObstaclesController.RemoveFromObstaclePassedNotifyer(ObstaclePassed);
        mTimeController.RemoveFromSecondsNotifier(SecondPassed);
        mPlayerController.RemoveFromOnBoostChange(BoostChanged);
        mGameStateController.RemoveFromGameStateChange(GameStateChange);
    }
}
