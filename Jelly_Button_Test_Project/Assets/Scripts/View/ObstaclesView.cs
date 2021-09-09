using System.Collections.Generic;
using UnityEngine;

public class ObstaclesView : MonoBehaviour, IObstaclesView
{
    IObstaclesController mController;

    [SerializeField]
    private Transform ObstacleInstance;

    private Queue<Transform> pool = new Queue<Transform>();
    private Queue<Transform> activeObstacles = new Queue<Transform>();

    public float ObstaclePositionY => ObstacleInstance.position.y;

    private void Awake()
    {
        mController = SingleManager.Get<IObstaclesController>();
        mController.SetView(this);
    }

    public void AddObstacle(Vector3 pos)
    {
        Transform p;
        if (pool.Count == 0)
            p = Instantiate(ObstacleInstance, ObstacleInstance.parent);
        else
            p = pool.Dequeue();
        activeObstacles.Enqueue(p);
        p.position = pos;
        p.gameObject.SetActive(true);
    }

    public void RemoveOldestObstacle()
    {
        Transform oldestPlane = activeObstacles.Dequeue();
        oldestPlane.gameObject.SetActive(false);
        pool.Enqueue(oldestPlane);
    }
}
