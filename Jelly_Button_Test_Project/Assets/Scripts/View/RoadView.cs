using System.Collections.Generic;
using UnityEngine;

public class RoadView : MonoBehaviour, IRoadView
{
    private IRoadController mController;

    [SerializeField]
    private Transform planeInstance;

    private Queue<Transform> pool = new Queue<Transform>();
    private Queue<Transform> activePlanes = new Queue<Transform>();

    public float PlaneSize => 10; //TODO get it from model

    public float PlanePositionY => planeInstance.position.y;

    private void Awake()
    {
        mController = SingleManager.Get<IRoadController>();
        mController.SetView(this);
    }

    public void AddPlane(Vector3 pos)
    {
        Transform p;
        if (pool.Count == 0)
            p = Instantiate(planeInstance, planeInstance.parent);
        else
            p = pool.Dequeue();
        activePlanes.Enqueue(p);
        p.position = pos;
        p.gameObject.SetActive(true);
    }

    public void RemoveOldestPlane()
    {
        Transform oldestPlane = activePlanes.Dequeue();
        oldestPlane.gameObject.SetActive(false);
        pool.Enqueue(oldestPlane);
    }
}
