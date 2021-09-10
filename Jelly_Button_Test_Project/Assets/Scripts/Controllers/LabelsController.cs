using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelsController : ILabelsController
{
    private ITimeController mTimeController;
    private ILabelsView mView;

    public LabelsController()
    {
        SingleManager.Register<ILabelsController>(this);
    }

    public void Init()
    {
        mTimeController = SingleManager.Get<ITimeController>();
    }

    public void SetView(ILabelsView view)
    {
        mView = view;
    }

    public void Update()
    {
        mView.TimeText = TimeSpan.FromSeconds(mTimeController.SecondsCounter).ToString("mm':'ss':'ff");
    }

    public void Destroy()
    {
        SingleManager.Remove<ILabelsController>();
    }
}
