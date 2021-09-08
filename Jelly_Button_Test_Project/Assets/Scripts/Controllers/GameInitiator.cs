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

        SingleManager.Get<IPlayerController>().StartGame();//TODO FIx:(
    }

    private void ConfigControllers()
    {
        controllers.Add(new PlayerController());
    }

    private void OnDestroy()
    {
        foreach (IController controller in controllers)
            controller.Destroy();
    }
    #endregion
}