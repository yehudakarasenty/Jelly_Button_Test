using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsibility: Move the player and listen to collosions
/// </summary>
public class PlayerController : IPlayerController
{
    #region Members
    #region Const
    private const float MAXIMUM_HORIZONTAL_MOVMENT_SPEED = 20;
    private const float MAXIMUM_ROTATION_AXIS = 45;
    private const float PLAYER_SPEED = 20;
    #endregion

    #region Dependencies
    private IInputListener mInputListener;
    private IRoadController mRoadController;
    private IGameStateController mGameStateController;
    #endregion

    private IPlayerView mView;

    public Vector3 PlayerPosition => mView.Position;

    private BoolUnityEvent onBoostChange = new BoolUnityEvent();

    private UnityEvent onPlayerCollided = new UnityEvent();

    private float speed;

    private bool boost = false;

    public Vector3 PlayerSize { get => mView.Size; }
    #endregion

    #region Functions
    public PlayerController()
    {
        SingleManager.Register<IPlayerController>(this);
    }

    public void Init()
    {
        mInputListener = SingleManager.Get<IInputListener>();
        mRoadController = SingleManager.Get<IRoadController>();
        mGameStateController = SingleManager.Get<IGameStateController>();

        mInputListener.RegisterToHorizontalInput(OnHorizontalInputChange);
        speed = PLAYER_SPEED;
    }

    public void SetView(IPlayerView playerView)
    {
        mView = playerView;
    }

    public void Update()
    {
        if (mGameStateController.GameState == GameState.PLAYING)
            FlyForword();
    }

    private void FlyForword()
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
        onBoostChange.Invoke(boost);
    }

    public void RegisterToOnBoostChange(UnityAction<bool> action) => onBoostChange.AddListener(action);

    public void RemoveFromOnBoostChange(UnityAction<bool> action) => onBoostChange.RemoveListener(action);

    public void RegisterToOnPlayerCollided(UnityAction action) => onPlayerCollided.AddListener(action);

    public void RemoveFromOnPlayerCollided(UnityAction action) => onPlayerCollided.RemoveListener(action);

    private void OnHorizontalInputChange(float axis)
    {
        if (mGameStateController.GameState == GameState.PLAYING)
        {
            //calculate position
            Vector3 position = mView.Position;
            float step = MAXIMUM_HORIZONTAL_MOVMENT_SPEED * axis * Time.deltaTime;
            if (Math.Abs(position.x + step) < mRoadController.RoadWidth / 2)
                mView.Position = new Vector3(position.x + step, position.y, position.z);

            //calculate rotation
            Vector3 rotation = mView.Rotation.eulerAngles;
            rotation.z = MAXIMUM_ROTATION_AXIS * axis * -1;
            mView.Rotation = Quaternion.Euler(rotation);
        }
    }

    public void PlyerCollided() => onPlayerCollided.Invoke();

    public void Destroy()
    {
        mInputListener.RemoveFromHorizontalInput(OnHorizontalInputChange);
        SingleManager.Remove<IPlayerController>();
    }
    #endregion
}

public class BoolUnityEvent : UnityEvent<bool> { }

