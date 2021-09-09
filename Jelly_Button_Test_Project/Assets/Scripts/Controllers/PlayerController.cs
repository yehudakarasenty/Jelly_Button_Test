using System;
using UnityEngine;

public class PlayerController : IPlayerController 
{
    private const float MAXIMUM_MOVMENT_SPEED = 20;
    private const float MAXIMUM_ROTATION_AXIS = 45;
    private const float PLAYER_SPPED = 20;

    #region Dependencis
    private IInputListener mInputListener;
    private IRoadController mRoadController;
    #endregion

    private IPlayerView mView;

    public Vector3 PlayerPosition => mView.Position;

    public PlayerController()
    {
        SingleManager.Register<IPlayerController>(this);
    }

    public void Init()
    {
        mInputListener = SingleManager.Get<IInputListener>();
        mRoadController = SingleManager.Get<IRoadController>();
    }

    public void SetView(IPlayerView playerView)
    {
        mView = playerView;
    }

    public void StartGame()
    {
        mInputListener.RegisterToHorizontalInput(OnHorizontalInputChange);
    }

    public void Update()
    {
        mView.Position += new Vector3(0, 0, PLAYER_SPPED * Time.deltaTime);
    }

    private void OnHorizontalInputChange(float axis)
    {
        Vector3 position = mView.Position;
        float step = MAXIMUM_MOVMENT_SPEED * axis * Time.deltaTime;
        if(Math.Abs(position.x + step)< mRoadController.RoadWidth/ 2)
            mView.Position = new Vector3(position.x + step, position.y, position.z);
        Vector3 rotation = mView.Rotation.eulerAngles;
        rotation.z = MAXIMUM_ROTATION_AXIS * axis * -1;
        mView.Rotation = Quaternion.Euler(rotation);
    }

    public void EndGame()
    {
        throw new NotImplementedException(); //TODO
    }

    public void Destroy()
    {
        SingleManager.Remove<IPlayerController>();
    }

}
