using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    #region Members
    private List<IController> controllers = new List<IController>();
    #endregion

    #region Functions
    private void Awake()
    {
        ConfigControllers();
    }

    private void Start()
    {
        foreach (IController controller in controllers)
            controller.Init();

        SingleManager.Get<IGameStateController>().AppInited();
    }

    private void ConfigControllers()
    {
        controllers.Add(new PlayerController());
        controllers.Add(new RoadController());
        controllers.Add(new ObstaclesController());
        controllers.Add(new SmoothFollowContoller());
        controllers.Add(new UiController());
        controllers.Add(new TimeController());
        controllers.Add(new ScoreController());
        controllers.Add(new GameStateController());
    }

    private void Update()
    {
        foreach (IController controller in controllers)
            controller.Update();
    }

    private void OnDestroy()
    {
        foreach (IController controller in controllers)
            controller.Destroy();
    }
    #endregion
}