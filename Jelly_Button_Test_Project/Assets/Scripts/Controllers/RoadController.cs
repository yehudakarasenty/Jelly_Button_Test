using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : IRoadController
{
    #region Dependencies
    private IPlayerController mPlayerController;
    #endregion

    private const float ROAD_LENGTH = 200;

    private IRoadView mView;

    private Queue<Vector3> planesPositions = new Queue<Vector3>();

    private Vector3 lastPlane;

    public RoadController()
    {
        SingleManager.Register<IRoadController>(this);
    }

    public void Init()
    {
        mPlayerController = SingleManager.Get<IPlayerController>();
        lastPlane = new Vector3(0, mView.PlanePositionY, 0);
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
        Vector3 newPosition = lastPlane + new Vector3(0, 0, mView.PlaneSize);
        planesPositions.Enqueue(newPosition);
        mView.AddPlane(newPosition);
        lastPlane = newPosition;
    }

    private bool NeedToBuild { get => lastPlane.z - mPlayerController.PlayerPosition.z < ROAD_LENGTH; }

    private bool NeedToDestroy { get => planesPositions.Count != 0 && mPlayerController.PlayerPosition.z - planesPositions.Peek().z > mView.PlaneSize; }

    public void SetView(IRoadView view)
    {
        mView = view;
    }

    public void StartGame()
    {
        
    }

    public void Destroy()
    {
        SingleManager.Remove<IRoadController>();
    }
}
