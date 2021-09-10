using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstaclesController : IObstaclesController
{
    private const float FIRST_DISTANCE_BETWEEN_OBSTACLES_Z = 50;

    private IRoadController mRoadController;
    private IPlayerController mPlayerController;

    private IObstaclesView mView;

    private float minimumDistanceBetweenObstaclesZ;

    private Vector3 lastCreatedObstaclePosition;

    private readonly Queue<Vector3> obstaclesPositions = new Queue<Vector3>();

    private static System.Random random = new System.Random();

    public bool CanAddNewObstacle { get => mPlayerController.PlayerPosition.z + mRoadController.RoadLength - lastCreatedObstaclePosition.z >= minimumDistanceBetweenObstaclesZ; }
    public bool NeedToDestroy { get => obstaclesPositions.Count != 0 && mPlayerController.PlayerPosition.z - obstaclesPositions.Peek().z > mPlayerController.PlayerSize.z; }

    public int PassedObstacleAmount { get; private set; }
    private readonly UnityEvent obstaclePassedEvent = new UnityEvent();

    public ObstaclesController()
    {
        SingleManager.Register<IObstaclesController>(this);
    }

    public void Init()
    {
        mRoadController = SingleManager.Get<IRoadController>();
        mPlayerController = SingleManager.Get<IPlayerController>();
        minimumDistanceBetweenObstaclesZ = FIRST_DISTANCE_BETWEEN_OBSTACLES_Z;
        lastCreatedObstaclePosition = new Vector3(0, mView.ObstaclePositionY, 0);
    }

    public void SetView(IObstaclesView view)
    {
        mView = view;
    }

    public void StartGame()
    {
        CreateFirstObstacles();
        PassedObstacleAmount = 0;
    }

    private void CreateFirstObstacles()
    {
        int obstacles = (int)Math.Ceiling(mRoadController.RoadLength / FIRST_DISTANCE_BETWEEN_OBSTACLES_Z);
        for (int i = 0; i < obstacles; i++)
            CreateObstacle();
    }

    private void CreateObstacle()
    {

        float disToAdd = GetRandomNumberInRange(0,20);
        float randomXPos = GetRandomNumberInRange(-mRoadController.RoadWidth / 2, mRoadController.RoadWidth/2);
        Vector3 newPosition =  new Vector3(randomXPos, 0, lastCreatedObstaclePosition.z + minimumDistanceBetweenObstaclesZ + disToAdd);
        obstaclesPositions.Enqueue(newPosition);
        mView.AddObstacle(newPosition);
        lastCreatedObstaclePosition = newPosition;
    }

    private void DestroyLastObstacle()
    {
        obstaclesPositions.Dequeue();
        mView.RemoveOldestObstacle();
        PassedObstacleAmount++;
        obstaclePassedEvent.Invoke();
    }

    private float GetRandomNumberInRange(double minNumber, double maxNumber)
    {
        double val = (random.NextDouble() * (maxNumber - minNumber) + minNumber);
        return (float)val;
    }

    public void Update()
    {
        if (CanAddNewObstacle)
            CreateObstacle();
        if (NeedToDestroy)
            DestroyLastObstacle();
    }

    public void Destroy()
    {
        SingleManager.Remove<IObstaclesController>();
    }

    public void RegisterToObstaclePassedNotifyer(UnityAction action)
    {
        obstaclePassedEvent.AddListener(action);
    }

    public void RemoveFromObstaclePassedNotifyer(UnityAction action)
    {
        obstaclePassedEvent.RemoveListener(action);
    }
}
