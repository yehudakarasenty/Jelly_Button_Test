using System;

public class LabelsController : ILabelsController
{
    private ITimeController mTimeController;
    private IScoreController mScoreController;
    private ILabelsView mView;

    public LabelsController()
    {
        SingleManager.Register<ILabelsController>(this);
    }

    public void Init()
    {
        mTimeController = SingleManager.Get<ITimeController>();
        mScoreController = SingleManager.Get<IScoreController>();
        mScoreController.RegisterToScoreChangeNotifyer(UpdateScore);
        mScoreController.RegisterToBestScoreChangeNotifyer(UpdateBestScore);
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

    private void UpdateScore()
    {
        mView.CurrentScoreText = "Score: " + mScoreController.CurrentScore;
    }

    private void UpdateBestScore()
    {
        mView.BestScoreText = "Best Score: " + mScoreController.BestScore;
    }

    public void Destroy()
    {
        SingleManager.Remove<ILabelsController>();
        mScoreController.RemoveFromScoreChangeNotifyer(UpdateScore);
    }
}
