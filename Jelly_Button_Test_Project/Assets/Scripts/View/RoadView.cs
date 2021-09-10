using System.Collections.Generic;
using UnityEngine;

public class RoadView : MonoBehaviour, IRoadView
{
    private IRoadController mController;

    [SerializeField]
    private MeshRenderer planeInstance;

    private readonly Queue<MeshRenderer> pool = new Queue<MeshRenderer>();
    private readonly Queue<MeshRenderer> activePlanes = new Queue<MeshRenderer>();

    public float PlaneSize => planeInstance.GetComponent<MeshRenderer>().bounds.size.z;

    public float PlanePositionY => planeInstance.transform.position.y;

    private void Awake()
    {
        mController = SingleManager.Get<IRoadController>();
        mController.SetView(this);
    }

    public void AddPlane(Vector3 pos)
    {
        MeshRenderer p;
        if (pool.Count == 0)
            p = Instantiate(planeInstance, planeInstance.transform.parent);
        else
            p = pool.Dequeue();
        activePlanes.Enqueue(p);
        p.transform.position = pos;
        p.gameObject.SetActive(true);
    }

    public void RemoveOldestPlane()
    {
        MeshRenderer oldestPlane = activePlanes.Dequeue();
        oldestPlane.gameObject.SetActive(false);
        pool.Enqueue(oldestPlane);
    }
}
