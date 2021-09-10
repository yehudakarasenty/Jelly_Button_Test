using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : IObstaclesController
{
    private const float FIRST_DISTANCE_BETWEEN_OBSTACLES_Z = 50;

    private IRoadController mRoadController;
    private IPlayerController mPlayerController;

    private IObstaclesView mView;

    private float minimumDistanceBetweenObstaclesZ;

    private Vector3 lastCreatedObstaclePosition;

    private Queue<Vector3> obstaclesPositions = new Queue<Vector3>();

    public bool CanAddNewObstacle { get => mPlayerController.PlayerPosition.z + mRoadController.RoadLength - lastCreatedObstaclePosition.z >= minimumDistanceBetweenObstaclesZ; }
    public bool NeedToDestroy { get => obstaclesPositions.Count != 0 && mPlayerController.PlayerPosition.z - obstaclesPositions.Peek().z > minimumDistanceBetweenObstaclesZ; }

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
    }

    private void CreateFirstObstacles()
    {
        int obstacles = (int)Math.Ceiling(mRoadController.RoadLength / FIRST_DISTANCE_BETWEEN_OBSTACLES_Z);
        for (int i = 0; i < obstacles; i++)
            CreateObstacle();
    }

    private void CreateObstacle()
    {
        if (NeedToDestroy)
        {
            obstaclesPositions.Dequeue();
            mView.RemoveOldestObstacle();
        }
        float disToAdd = GetRandomNumberInRange(0,20);
        float randomXPos = GetRandomNumberInRange(-mRoadController.RoadWidth / 2, mRoadController.RoadWidth/2);
        Vector3 newPosition =  new Vector3(randomXPos, 0, lastCreatedObstaclePosition.z + minimumDistanceBetweenObstaclesZ + disToAdd);
        obstaclesPositions.Enqueue(newPosition);
        mView.AddObstacle(newPosition);
        lastCreatedObstaclePosition = newPosition;
    }

    private float GetRandomNumberInRange(double minNumber, double maxNumber)
    {
        return (float)(new System.Random().NextDouble() * (maxNumber - minNumber) + minNumber);
    }

    public void Update()
    {
        if (CanAddNewObstacle)
            CreateObstacle();
    }

    public void Destroy()
    {
        SingleManager.Remove<IObstaclesController>();
    }
}
