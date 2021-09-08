using System;
using UnityEngine;

public class PlayerController : IPlayerController
{
    private const float ROAD_WIDTH = 200;
    private const float MAXIMUM_MOVMENT_SPEED = 20;
    private const float MAXIMUM_ROTATION_AXIS = 45;

    #region Dependencis
    private IInputListener mInputListener;
    #endregion

    private IPlayerView mView;

    public PlayerController()
    {
        SingleManager.Register<IPlayerController>(this);
    }

    public void Init()
    {
        mInputListener = SingleManager.Get<IInputListener>();
    }

    public void SetView(IPlayerView playerView)
    {
        mView = playerView;
    }

    public void StartGame()
    {
        mInputListener.RegisterToHorizontalInput(OnHorizontalInputChange);
    }

    private void OnHorizontalInputChange(float axis)
    {
        Vector3 position = mView.Position;
        float step = MAXIMUM_MOVMENT_SPEED * axis * Time.deltaTime;
        //TODO check if its in the road width
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
