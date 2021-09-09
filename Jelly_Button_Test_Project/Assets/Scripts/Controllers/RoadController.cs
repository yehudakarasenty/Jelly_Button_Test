﻿using System.Collections.Generic;
using UnityEngine;

public class RoadController : IRoadController
{
    private const float ROAD_LENGTH = 200;
    private const float ROAD_WIDTH = 6;

    #region Dependencies
    private IPlayerController mPlayerController;
    #endregion

    private IRoadView mView;

    private Queue<Vector3> planesPositions = new Queue<Vector3>();

    private Vector3 lastCreatedPlane;

    private bool NeedToBuild { get => lastCreatedPlane.z - mPlayerController.PlayerPosition.z < ROAD_LENGTH; }

    private bool NeedToDestroy { get => planesPositions.Count != 0 && mPlayerController.PlayerPosition.z - planesPositions.Peek().z > mView.PlaneSize; }

    public float RoadLength => ROAD_LENGTH;

    public float RoadWidth => ROAD_WIDTH;

    public RoadController()
    {
        SingleManager.Register<IRoadController>(this);
    }

    public void Init()
    {
        mPlayerController = SingleManager.Get<IPlayerController>();
        lastCreatedPlane = new Vector3(0, mView.PlanePositionY, 0);
    }

    public void SetView(IRoadView view)
    {
        mView = view;
    }

    public void StartGame()
    {
        BuildFirstRoad();
    }

    private void BuildFirstRoad()
    {
        while (NeedToBuild)
            AddPlane();
    }

    public void Update()
    {
        if (NeedToBuild)
            AddPlane();
    }

    private void AddPlane()
    {
        if (NeedToDestroy)
        {
            planesPositions.Dequeue();
            mView.RemoveOldestPlane();
        }
        Vector3 newPosition = lastCreatedPlane + new Vector3(0, 0, mView.PlaneSize);
        planesPositions.Enqueue(newPosition);
        mView.AddPlane(newPosition);
        lastCreatedPlane = newPosition;
    }

    public void Destroy()
    {
        SingleManager.Remove<IRoadController>();
    }
}
