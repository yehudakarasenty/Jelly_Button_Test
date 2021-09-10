using System;

public class LabelsController : ILabelsController
{
    private ITimeController mTimeController;
    private IScoreController mScoreController;
    private IObstaclesController mObstaclesController;
    private ILabelsView mView;

    public LabelsController()
    {
        SingleManager.Register<ILabelsController>(this);
    }

    public void Init()
    {
        mTimeController = SingleManager.Get<ITimeController>();
        mScoreController = SingleManager.Get<IScoreController>();
        mObstaclesController = SingleManager.Get<IObstaclesController>();

        mScoreController.RegisterToScoreChangeNotifyer(UpdateScore);
        mScoreController.RegisterToBestScoreChangeNotifyer(UpdateBestScore);
        mObstaclesController.RegisterToObstaclePassedNotifyer(UpdateObstaclesAmount);
    }

    public void SetView(ILabelsView view)
    {
        mView = view;
        UpdateBestScore();
    }

    public void Update()
    {
        mView.TimeText = TimeSpan.FromSeconds(mTimeController.SecondsCounter).ToString("mm':'ss':'ff");
    }

    private void UpdateScore()=> mView.CurrentScoreText = "Score: " + mScoreController.CurrentScore;

    private void UpdateBestScore()=> mView.BestScoreText = "Best Score: " + mScoreController.BestScore;

    private void UpdateObstaclesAmount()=>  mView.ObstaclesText = "Asteroids: "+ mObstaclesController.PassedObstacleAmount;

    public void Destroy()
    {
        SingleManager.Remove<ILabelsController>();
        mScoreController.RemoveFromScoreChangeNotifyer(UpdateScore);
        mScoreController.RemoveFromBestScoreChangeNotifyer(UpdateBestScore);
        mObstaclesController.RemoveFromObstaclePassedNotifyer(UpdateObstaclesAmount);
    }
}
