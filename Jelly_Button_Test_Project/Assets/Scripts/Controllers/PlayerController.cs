using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : IPlayerController 
{
    private const float MAXIMUM_MOVMENT_SPEED = 20;
    private const float MAXIMUM_ROTATION_AXIS = 45;
    private const float PLAYER_SPEED = 20;

    #region Dependencis
    private IInputListener mInputListener;
    private IRoadController mRoadController;
    #endregion

    private IPlayerView mView;

    public Vector3 PlayerPosition => mView.Position;

    private BoolUnityEvent OnBoostChange = new BoolUnityEvent();

    private float speed;
    private bool boost = false;

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
        speed = PLAYER_SPEED;
    }

    public void Update()
    {
        bool boost = mInputListener.IsKeyIsDown(KeyCode.Space);
        if (this.boost != boost)
            SetBoost(boost);
        mView.Position += new Vector3(0, 0, speed * Time.deltaTime);
    }

    private void SetBoost(bool boost)
    {
        this.boost = boost;
        speed = PLAYER_SPEED * (boost ? 2 : 1);
        OnBoostChange.Invoke(boost);
    }

    public void RegisterToOnBoostChange(UnityAction<bool> action)
    {
        OnBoostChange.AddListener(action);
    }

    public void RemoveFromOnBoostChange(UnityAction<bool> action)
    {
        OnBoostChange.RemoveListener(action);
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

public class BoolUnityEvent : UnityEvent<bool> { }

