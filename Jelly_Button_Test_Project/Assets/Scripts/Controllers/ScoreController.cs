using UnityEngine;
using UnityEngine.Events;

public class ScoreController : IScoreController
{
    private IObstaclesController mObstaclesController;
    private ITimeController mTimeController;
    private IPlayerController mPlayerController;

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

        mObstaclesController.RegisterToObstaclePassedNotifyer(ObstaclePassed);
        mTimeController.RegisterToSecondsNotifier(SecondPassed);
        mPlayerController.RegisterToOnBoostChange(BoostChanged);

        BestScore = PlayerPrefs.GetInt("best_score", 0);
    }

    public void StartGame()
    {
        CurrentScore = 0;
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

    public void EndGame()
    {
        if (BestScore > PlayerPrefs.GetInt("best_score", 0))
            PlayerPrefs.SetInt("best_score", BestScore);
    }

    public void Destroy()
    {
        SingleManager.Remove<IScoreController>();
        mObstaclesController.RemoveFromObstaclePassedNotifyer(ObstaclePassed);
        mTimeController.RemoveFromSecondsNotifier(SecondPassed);
        mPlayerController.RemoveFromOnBoostChange(BoostChanged);
    }
}
