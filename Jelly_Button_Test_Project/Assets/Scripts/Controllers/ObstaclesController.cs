using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstaclesController : IObstaclesController
{
    private const float MAXIMUM_DISTANCE_BETWEEN_OBSTACLES_Z = 50;
    private const float MINIMUM_DISTANCE_BETWEEN_OBSTACLES_Z = 10;
    private const float SECONDS_UNTIL_HIGHEST_DIFFICULTY = 120;
    private const float MAXIMUM_DISTANCE_TO_ADD_TO_RANDOM_RESULTES = 20;
    private const float MINIMUM_DISTANCE_TO_ADD_TO_RANDOM_RESULTES = 10;

    private IRoadController mRoadController;
    private IPlayerController mPlayerController;
    private ITimeController mTimeController;

    private IObstaclesView mView;

    private float minimumDistanceBetweenObstaclesZ;
    private float maxDistanceToAddRandom;

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
        mTimeController = SingleManager.Get<ITimeController>();

        mTimeController.RegisterToSecondsNotifier(UpdateDifficulty);
        minimumDistanceBetweenObstaclesZ = MAXIMUM_DISTANCE_BETWEEN_OBSTACLES_Z;
        maxDistanceToAddRandom = MAXIMUM_DISTANCE_TO_ADD_TO_RANDOM_RESULTES;
        lastCreatedObstaclePosition = new Vector3(0, mView.ObstaclePositionY, 0);
        CreateFirstObstacles();
        PassedObstacleAmount = 0;
    }

    public void SetView(IObstaclesView view)
    {
        mView = view;
    }

    private void CreateFirstObstacles()
    {
        int obstacles = (int)Math.Ceiling(mRoadController.RoadLength / MAXIMUM_DISTANCE_BETWEEN_OBSTACLES_Z);
        for (int i = 0; i < obstacles; i++)
            CreateObstacle();
    }

    private void CreateObstacle()
    {
        float disToAdd = GetRandomNumberInRange(0, maxDistanceToAddRandom);
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

    public void RegisterToObstaclePassedNotifyer(UnityAction action)
    {
        obstaclePassedEvent.AddListener(action);
    }

    public void RemoveFromObstaclePassedNotifyer(UnityAction action)
    {
        obstaclePassedEvent.RemoveListener(action);
    }

    private void UpdateDifficulty()
    {
        if (minimumDistanceBetweenObstaclesZ > MINIMUM_DISTANCE_BETWEEN_OBSTACLES_Z)
        {
            float difficultyPrecentage = Math.Min(mTimeController.SecondsCounter / SECONDS_UNTIL_HIGHEST_DIFFICULTY, 1);
            minimumDistanceBetweenObstaclesZ = Mathf.Lerp(MAXIMUM_DISTANCE_BETWEEN_OBSTACLES_Z, MINIMUM_DISTANCE_BETWEEN_OBSTACLES_Z, difficultyPrecentage);
            maxDistanceToAddRandom = Mathf.Lerp(MAXIMUM_DISTANCE_TO_ADD_TO_RANDOM_RESULTES, MINIMUM_DISTANCE_TO_ADD_TO_RANDOM_RESULTES, difficultyPrecentage);
        } 
    }

    public void Destroy()
    {
        SingleManager.Remove<IObstaclesController>();
        mTimeController.RemoveFromSecondsNotifier(UpdateDifficulty);
    }
}
