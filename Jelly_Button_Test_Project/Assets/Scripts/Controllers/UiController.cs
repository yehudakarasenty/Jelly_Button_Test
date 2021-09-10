using System;

public class UiController : IUiController
{
    private ITimeController mTimeController;
    private IScoreController mScoreController;
    private IObstaclesController mObstaclesController;
    private IGameStateController mGameStateController;
    private IUiView mView;

    public UiController()
    {
        SingleManager.Register<IUiController>(this);
    }

    public void Init()
    {
        mTimeController = SingleManager.Get<ITimeController>();
        mScoreController = SingleManager.Get<IScoreController>();
        mObstaclesController = SingleManager.Get<IObstaclesController>();
        mGameStateController = SingleManager.Get<IGameStateController>();

        mScoreController.RegisterToScoreChangeNotifyer(UpdateScore);
        mScoreController.RegisterToBestScoreChangeNotifyer(UpdateBestScore);
        mObstaclesController.RegisterToObstaclePassedNotifyer(UpdateObstaclesAmount);
        mGameStateController.RegisterToGameStateChange(GameStateChange);
    }

    private void GameStateChange()
    {
        switch (mGameStateController.GameState)
        {
            case GameState.APP_INITED:
                UpdateBestScore();
                mView.ShowBestScoreText = true;
                mView.ShowPressOnAnyKeyText = true;
                break;
            case GameState.PLAYING:
                mView.ShowPressOnAnyKeyText = false;
                mView.ShowCurrentScoreText = true;
                mView.ShowObstaclesText = true;
                mView.ShowTimeText = true;
                break;
            case GameState.GAME_OVER:
                mView.ShowPressOnAnyKeyText = true; //TODO: 
                mView.ShowCurrentScoreText = false;
                mView.ShowObstaclesText = false;
                mView.ShowTimeText = false;
                mView.ShowGameOver = true;
                break;
            default:
                break;
        }
    }

    public void SetView(IUiView view)
    {
        mView = view;
    }

    public void Update()
    {
        if (mGameStateController.GameState == GameState.PLAYING)
            mView.TimeText = TimeSpan.FromSeconds(mTimeController.SecondsCounter).ToString("mm':'ss':'ff");
    }

    private void UpdateScore()=> mView.CurrentScoreText = "Score: " + mScoreController.CurrentScore;

    private void UpdateBestScore()=> mView.BestScoreText = "Best Score: " + mScoreController.BestScore;

    private void UpdateObstaclesAmount()=>  mView.ObstaclesText = "Asteroids: "+ mObstaclesController.PassedObstacleAmount;

    public void Destroy()
    {
        SingleManager.Remove<IUiController>();
        mScoreController.RemoveFromScoreChangeNotifyer(UpdateScore);
        mScoreController.RemoveFromBestScoreChangeNotifyer(UpdateBestScore);
        mObstaclesController.RemoveFromObstaclePassedNotifyer(UpdateObstaclesAmount);
        mGameStateController.RemoveFromGameStateChange(GameStateChange);
    }
}
