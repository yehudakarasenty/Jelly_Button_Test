/// <summary>
/// Responsibility: Manage the camera movment class
/// </summary>
public class SmoothFollowContoller : ISmoothFollowController
{
    #region Members
    #region Dependecies
    private IPlayerController mPlayerController;
    #endregion

    private ISmoothFollow mView;
    #endregion

    #region Functions
    public SmoothFollowContoller()
    {
        SingleManager.Register<ISmoothFollowController>(this);
    }

    public void Init()
    {
        mPlayerController = SingleManager.Get<IPlayerController>();
        mPlayerController.RegisterToOnBoostChange(SetZoom);
    }

    public void SetView(ISmoothFollow view)
    {
        mView = view;
    }

    public void SetZoom(bool zoom)
    {
        mView.SetDistance(zoom ? 4 : 6);
        mView.SetHeight(zoom ? 0.2f : 1);
    }

    public void Update(){}

    public void Destroy()
    {
        mPlayerController.RemoveFromOnBoostChange(SetZoom);
        SingleManager.Remove<ISmoothFollowController>();
    }
    #endregion
}
